using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    [Header("Refs")]
    public ForgeItem forgeItem;
    public WeaponDatabase weaponDatabase;

    [Header("Progressão")]
    public int level = 1;
    public double baseForgeHP = 50;
    public double hpGrowth = 1.12;

    [Header("Economia")]
    public double credits = 0;          // <-- ADICIONADO
    public double baseCredits = 10;     // <-- ADICIONADO
    public double creditsGrowth = 1.10; // <-- ADICIONADO
    public double manaEssence = 0;

    [Header("Pedidos")]
    public OrderData currentOrder;
    public WeaponSpec currentWeaponSpec;
    [Range(0f,1f)] public float rareOrderChance = 0.05f;
    public float rareOrderChanceIncrement = 0.05f;

    [Header("Dano")]
    public double tapPower = 1;
    public double dps = 0;

    [Header("Upgrades")]
    public double rankChanceBonus = 0;
    public double negativeRngReduction = 0;
    public Dictionary<WeaponType, WeaponUpgradeStats> weaponUpgrades = new Dictionary<WeaponType, WeaponUpgradeStats>();

    void Awake()
    {
        if (I != null) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        SaveSystem.Load();
        UIController.I?.UpdateCredits(credits);
    }

    void Update()
    {
        if (currentOrder != null && dps > 0 && forgeItem != null && forgeItem.IsInProgress)
        {
            forgeItem.ApplyProgress(dps * Time.deltaTime);
        }
    }

    public void HammerTap()
    {
        if (forgeItem == null || currentOrder == null) return;
        forgeItem.ApplyProgress(tapPower);
        UIController.I?.SpawnFloatingText($"-{tapPower:0}", forgeItem.transform.position, Color.red);
        if (!forgeItem.IsInProgress)
        {
            // Avalia a qualidade da forja para aplicar multiplicador de pagamento
            float qualityScore = Random.Range(0f, 100f); // TODO: substituir pelo cálculo real da qualidade
            RankSystem.RankEvaluation eval = RankSystem.Evaluate(qualityScore);
            UIController.I?.ShowWeaponResult(currentWeaponSpec, eval.rank);
        }
    }

    public OrderData GenerateOrder()
    {
        if (weaponDatabase == null || weaponDatabase.weaponSpecs.Length == 0)
            return null;
        var specs = weaponDatabase.weaponSpecs;
        var spec = specs[Random.Range(0, specs.Length)];
        bool vip = Random.value < rareOrderChance;
        int min = Random.Range(10, 20);
        int max = Random.Range(min, min + 20);
        return new OrderData { weaponSpec = spec, minPayment = min, maxPayment = max, requiresAd = vip };
    }

    public void StartOrder(OrderData order)
    {
        currentOrder = order;
        currentWeaponSpec = order != null ? order.weaponSpec : null;
        if (currentWeaponSpec != null)
        {
            forgeItem.Setup(currentWeaponSpec, false);
        }
    }

    public void CompleteOrder(Rank rank, int payment)
    {
        if (currentOrder == null) return;

        credits += payment;
        manaEssence += payment;

        if (UIController.I != null)
        {
            UIController.I.UpdateCredits(credits);
            if (UIController.I.creditsTxt)
                UIController.I.SpawnFloatingText($"+{payment:0}", UIController.I.creditsTxt.transform.position, Color.yellow);
        }

        Debug.Log($"[Order] {rank} +{payment:0} cr (total {credits:0})");

        if (rank == Rank.APlus)
        {
            rareOrderChance = Mathf.Min(rareOrderChance + rareOrderChanceIncrement, 1f);
        }

        if (currentOrder != null && currentOrder.requiresAd)
        {
            VIPGallery.I?.Add(currentOrder);
        }

        currentOrder = null;
        currentWeaponSpec = null;

        SaveSystem.Save();
    }

    public int ComputePayment(int min, int max, Rank rank)
    {
        int basePayment = Random.Range(min, max + 1);
        int totalRanks = System.Enum.GetValues(typeof(Rank)).Length;
        float step = 100f / totalRanks;
        float qualityScore = ((int)rank + 0.5f) * step;
        RankSystem.RankEvaluation eval = RankSystem.Evaluate(qualityScore);
        return Mathf.RoundToInt(basePayment * eval.multiplier);
    }

    [System.Serializable]
    public class WeaponUpgradeStats
    {
        public double tapBonus = 0;
        public double dpsBonus = 0;
        public float timeModifier = 0;
        public float stabilityBonus = 0;
        public double valueBonus = 0;
    }

    public WeaponUpgradeStats GetWeaponStats(WeaponType type)
    {
        if (!weaponUpgrades.TryGetValue(type, out var stats))
        {
            stats = new WeaponUpgradeStats();
            weaponUpgrades[type] = stats;
        }
        return stats;
    }
}
