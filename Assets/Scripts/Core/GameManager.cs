using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    [Header("Refs")]
    public ForgeItem forgeItem;

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
        credits += reward;

        UIController.I?.UpdateCredits(credits);                  // <-- add
        Debug.Log($"[Forge] L{level} +{reward:0} cr (total {credits:0})");
    }


    void SetupLevel()
    {
        double hp = baseForgeHP * System.Math.Pow(hpGrowth, level - 1);
        bool legendary = (level % 10 == 0);
        forgeItem.Setup(hp, legendary);

        UIController.I?.UpdateLevel(level, legendary);           // <-- add
        UIController.I?.UpdateTapDps(tapPower, dps);             // <-- add (opcional)
    }

}
