using UnityEngine;
using TMPro;

public class UpgradeSystem : MonoBehaviour
{
    public TextMeshProUGUI tapCostTxt, tapValueTxt, dpsCostTxt, dpsValueTxt, botCostTxt, botValueTxt;

    public double tapBase = 15, tapGrowth = 1.15, tapPerBuy = 1;  int tapLv = 0;
    public double dpsBase = 20, dpsGrowth = 1.20, dpsPerBuy = 0.5; int dpsLv = 0;
    public double botBase = 30, botGrowth = 1.25, botPerBuy = 1.0; int botLv = 0;

    void Start() => Refresh();

    public void BuyTap() { TryBuy(ref tapLv, tapBase, tapGrowth, tapPerBuy, isTap:true); }
    public void BuyDps() { TryBuy(ref dpsLv, dpsBase, dpsGrowth, dpsPerBuy, isTap:false); }
    public void BuyBot() { TryBuy(ref botLv, botBase, botGrowth, botPerBuy, isTap:false); }

    void TryBuy(ref int level, double baseCost, double growth, double delta, bool isTap)
    {
        double cost = baseCost * System.Math.Pow(growth, level);
        if (GameManager.I.credits < cost) return;
        GameManager.I.credits -= cost;
        if (isTap) GameManager.I.tapPower += delta; else GameManager.I.dps += delta;
        level++; Refresh();
    }

    void Refresh()
    {
        if (tapCostTxt) tapCostTxt.text = $"Comprar ({tapBase * System.Math.Pow(tapGrowth, tapLv):0})";
        if (tapValueTxt) tapValueTxt.text = $"+{tapPerBuy:0} Tap";
        if (dpsCostTxt) dpsCostTxt.text = $"Comprar ({dpsBase * System.Math.Pow(dpsGrowth, dpsLv):0})";
        if (dpsValueTxt) dpsValueTxt.text = $"+{dpsPerBuy:0.##} DPS";
        if (botCostTxt) botCostTxt.text = $"Comprar ({botBase * System.Math.Pow(botGrowth, botLv):0})";
        if (botValueTxt) botValueTxt.text = $"+{botPerBuy:0.#} DPS";
        UIController.I?.UpdateTapDps(GameManager.I.tapPower, GameManager.I.dps);
        UIController.I?.UpdateCredits(GameManager.I.credits);
    }
}
