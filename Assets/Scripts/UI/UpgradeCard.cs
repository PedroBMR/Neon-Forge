using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeCard : MonoBehaviour
{
    public TextMeshProUGUI titleTxt;
    public TextMeshProUGUI descTxt;
    public TextMeshProUGUI costTxt;
    public TextMeshProUGUI levelTxt;

    UpgradeSystem.UpgradeData data;
    UpgradeSystem system;

    public void Setup(UpgradeSystem system, UpgradeSystem.UpgradeData data)
    {
        this.system = system;
        this.data = data;
        var btn = GetComponentInChildren<Button>();
        if (btn != null)
            btn.onClick.AddListener(OnBuy);
        Refresh();
    }

    void OnBuy()
    {
        system.TryBuy(data);
        Refresh();
    }

    void Refresh()
    {
        if (titleTxt) titleTxt.text = data.GetTitle();
        if (descTxt) descTxt.text = data.GetDescription();
        if (costTxt) costTxt.text = data.GetCost().ToString("0");
        if (levelTxt) levelTxt.text = $"Lv {data.level}";
    }
}
