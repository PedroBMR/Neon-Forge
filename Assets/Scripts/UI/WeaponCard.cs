using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponCard : MonoBehaviour
{
    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI costTxt;
    public Button buyButton;
    public GameObject lockObj;
    public TextMeshProUGUI lockLabel;

    WeaponGrid.WeaponEntry data;
    WeaponGrid grid;

    public void Setup(WeaponGrid grid, WeaponGrid.WeaponEntry data)
    {
        this.grid = grid;
        this.data = data;
        if (buyButton == null)
            buyButton = GetComponentInChildren<Button>();
        if (buyButton != null)
            buyButton.onClick.AddListener(OnBuy);
        Refresh();
    }

    void OnBuy()
    {
        grid.TryUnlock(data);
        Refresh();
    }

    void Refresh()
    {
        if (nameTxt) nameTxt.text = data.name;
        bool unlocked = grid.IsUnlocked(data.weaponType);
        if (data.isPremium)
        {
            if (costTxt) costTxt.text = "";
            if (buyButton) buyButton.interactable = false;
            if (lockObj) lockObj.SetActive(true);
            if (lockLabel) lockLabel.text = "Pacote";
            return;
        }
        if (lockObj) lockObj.SetActive(false);
        if (unlocked)
        {
            if (costTxt) costTxt.text = "Desbloqueado";
            if (buyButton) buyButton.interactable = false;
        }
        else
        {
            if (costTxt) costTxt.text = data.cost.ToString();
            if (buyButton) buyButton.interactable = true;
        }
    }
}
