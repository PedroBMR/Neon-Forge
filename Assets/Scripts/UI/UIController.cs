using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController I;
    public TextMeshProUGUI levelTxt, creditsTxt, tapDpsTxt;

    void Awake() => I = this;

    public void UpdateLevel(int level, bool legendary)
    {
        if (levelTxt) levelTxt.text = legendary ? $"Projeto Lendário – Nível {level}" : $"Projeto – Nível {level}";
    }
    public void UpdateCredits(double cr) { if (creditsTxt) creditsTxt.text = $"{cr:0}"; }
    public void UpdateTapDps(double tap, double dps) { if (tapDpsTxt) tapDpsTxt.text = $"TAP {tap:0} | DPS {dps:0.##}"; }
}
