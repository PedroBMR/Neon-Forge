using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ForgeItem : MonoBehaviour
{
    public double maxHP = 50;
    public double progress = 0;
    public bool isLegendary = false;
    public bool IsInProgress => progress < maxHP;

    [Header("UI")]
    public Slider forgeBar;        // arraste o Slider da UI
    public TextMeshProUGUI hpText; // arraste o TMP "0/0"

    public void Setup(double newHP, bool legendary)
    {
        maxHP = newHP; isLegendary = legendary; progress = 0;
        UpdateUI();
    }

    public void ApplyProgress(double amount)
    {
        if (!IsInProgress) return;
        progress = System.Math.Min(maxHP, progress + amount);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (forgeBar) forgeBar.value = (float)(progress / maxHP);
        if (hpText)   hpText.text  = $"{progress:0}/{maxHP:0}";
    }
}
