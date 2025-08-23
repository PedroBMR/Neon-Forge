using UnityEngine;

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

    [Header("Dano")]
    public double tapPower = 1;
    public double dps = 0;

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

}
