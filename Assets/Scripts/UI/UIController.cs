using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController I;
    public TextMeshProUGUI levelTxt, creditsTxt, tapDpsTxt;
    public TextMeshProUGUI orderWeaponTxt, orderRankTxt, orderPaymentTxt;
    public FloatingText floatingTextPrefab;
    public WeaponResultPanel weaponResultPanelPrefab;

    OrderData pendingOrder;

    void Awake() => I = this;

    public void UpdateLevel(int level, bool legendary)
    {
        if (levelTxt) levelTxt.text = legendary ? $"Projeto Lendário – Nível {level}" : $"Projeto – Nível {level}";
    }
    public void UpdateCredits(double cr) { if (creditsTxt) creditsTxt.text = $"{cr:0}"; }
    public void UpdateTapDps(double tap, double dps) { if (tapDpsTxt) tapDpsTxt.text = $"TAP {tap:0} | DPS {dps:0.##}"; }

    public void ShowOrder(OrderData order)
    {
        pendingOrder = order;
        if (order == null || order.weaponSpec == null) return;
        string weaponName = order.weaponSpec.weaponType.ToString();
        if (order.requiresAd) weaponName = $"[VIP] {weaponName}";
        if (orderWeaponTxt) orderWeaponTxt.text = weaponName;
        if (orderRankTxt) orderRankTxt.text = order.weaponSpec.rank.ToString();
        if (orderPaymentTxt) orderPaymentTxt.text = $"{order.minPayment} - {order.maxPayment}";
    }

    public void AcceptOrder()
    {
        if (pendingOrder == null) return;
        if (pendingOrder.requiresAd)
        {
            WatchAdAndAccept();
            return;
        }
        GameManager.I?.StartOrder(pendingOrder);
    }

    public void DeclineOrder()
    {
        ShowOrder(GenerateOrder());
    }

    OrderData GenerateOrder()
    {
        if (GameManager.I == null)
            return null;
        return GameManager.I.GenerateOrder();
    }

    void WatchAdAndAccept()
    {
        Debug.Log("Assistindo anúncio para pedido VIP... (placeholder)");
        GameManager.I?.StartOrder(pendingOrder);
    }

    public void ShowWeaponResult(WeaponSpec spec, Rank rank)
    {
        var prefab = weaponResultPanelPrefab;
        if (prefab == null)
            prefab = Resources.Load<WeaponResultPanel>("WeaponResultPanel");
        if (prefab == null) return;
        var panel = Instantiate(prefab, transform);
        panel.Show(spec, rank);
    }

    public void SpawnFloatingText(string message, Vector3 position, Color? color = null)
    {
        if (floatingTextPrefab == null) return;
        var ft = Instantiate(floatingTextPrefab, transform);
        ft.Show(message, position, color);
    }
}
