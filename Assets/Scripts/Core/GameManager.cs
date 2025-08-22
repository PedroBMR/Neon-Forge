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
        SetupLevel();
        UIController.I?.UpdateCredits(credits);
    }

    void Update()
    {
        if (dps > 0 && forgeItem != null && forgeItem.IsInProgress)
        {
            forgeItem.ApplyProgress(dps * Time.deltaTime);
        }
    }

    public void HammerTap()
    {
        if (forgeItem == null) return;
        forgeItem.ApplyProgress(tapPower);
        UIController.I?.SpawnFloatingText($"-{tapPower:0}", forgeItem.transform.position, Color.red);
        if (!forgeItem.IsInProgress)
        {
            AwardReward();      // <-- ADICIONADO
            level++;
            SaveSystem.Save();
            SetupLevel();
        }
    }

    void AwardReward()
    {
        double reward = baseCredits * System.Math.Pow(creditsGrowth, level - 1);
        if (forgeItem != null && forgeItem.isLegendary) reward *= 2.0;

        // Avalia a qualidade da forja para aplicar multiplicador de pagamento
        float qualityScore = Random.Range(0f, 100f); // TODO: substituir pelo cálculo real da qualidade
        RankSystem.RankEvaluation eval = RankSystem.Evaluate(qualityScore);
        reward *= eval.multiplier;
        credits += reward;

        if (UIController.I != null)
        {
            UIController.I.UpdateCredits(credits);                  // <-- add
            if (UIController.I.creditsTxt)
                UIController.I.SpawnFloatingText($"+{reward:0}", UIController.I.creditsTxt.transform.position, Color.yellow);
        }
        Debug.Log($"[Forge] L{level} {eval.rank} +{reward:0} cr (total {credits:0})");
    }


    void SetupLevel()
    {
        WeaponSpec spec = null;
        if (weaponDatabase != null && weaponDatabase.weaponSpecs != null && weaponDatabase.weaponSpecs.Length > 0)
        {
            int index = Mathf.Clamp(level - 1, 0, weaponDatabase.weaponSpecs.Length - 1);
            spec = weaponDatabase.weaponSpecs[index];
        }
        else
        {
            spec = ScriptableObject.CreateInstance<WeaponSpec>();
            spec.hp = baseForgeHP * System.Math.Pow(hpGrowth, level - 1);
            spec.time = 0f;
        }
        bool legendary = (level % 10 == 0);
        forgeItem.Setup(spec, legendary);

        UIController.I?.UpdateLevel(level, legendary);           // <-- add
        UIController.I?.UpdateTapDps(tapPower, dps);             // <-- add (opcional)
    }

}
