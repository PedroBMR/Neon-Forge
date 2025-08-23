using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponResultPanel : MonoBehaviour
{
    public TextMeshProUGUI damageTxt;
    public TextMeshProUGUI durabilityTxt;
    public TextMeshProUGUI speedTxt;
    public TextMeshProUGUI materialsTxt;
    public TextMeshProUGUI weightTimeTxt;
    public TextMeshProUGUI rankTxt;
    public TextMeshProUGUI extraRngTxt;
    public Button deliverButton;
    public Button saveGalleryButton;

    WeaponSpec spec;
    Rank rank;

    void Awake()
    {
        if (deliverButton != null)
            deliverButton.onClick.AddListener(OnDeliver);
        if (saveGalleryButton != null)
            saveGalleryButton.onClick.AddListener(OnSaveGallery);
    }

    public void Show(WeaponSpec spec, Rank rank)
    {
        this.spec = spec;
        this.rank = rank;

        if (damageTxt == null) damageTxt = transform.Find("DamageTxt")?.GetComponent<TextMeshProUGUI>();
        if (durabilityTxt == null) durabilityTxt = transform.Find("DurabilityTxt")?.GetComponent<TextMeshProUGUI>();
        if (speedTxt == null) speedTxt = transform.Find("SpeedTxt")?.GetComponent<TextMeshProUGUI>();
        if (materialsTxt == null) materialsTxt = transform.Find("MaterialsTxt")?.GetComponent<TextMeshProUGUI>();
        if (weightTimeTxt == null) weightTimeTxt = transform.Find("WeightTimeTxt")?.GetComponent<TextMeshProUGUI>();
        if (rankTxt == null) rankTxt = transform.Find("RankTxt")?.GetComponent<TextMeshProUGUI>();
        if (extraRngTxt == null) extraRngTxt = transform.Find("ExtraRngTxt")?.GetComponent<TextMeshProUGUI>();
        if (deliverButton == null) deliverButton = transform.Find("DeliverButton")?.GetComponent<Button>();
        if (saveGalleryButton == null) saveGalleryButton = transform.Find("SaveGalleryButton")?.GetComponent<Button>();

        if (spec != null)
        {
            if (damageTxt) damageTxt.text = spec.hp.ToString("0");
            if (durabilityTxt) durabilityTxt.text = spec.hp.ToString("0");
            if (speedTxt) speedTxt.text = spec.time.ToString("0.##");
            if (materialsTxt) materialsTxt.text = spec.weaponType.ToString();
            if (weightTimeTxt) weightTimeTxt.text = spec.time.ToString("0.##");
        }

        if (rankTxt) rankTxt.text = rank.ToString();
        if (extraRngTxt) extraRngTxt.text = Random.Range(0f, 100f).ToString("0");

        if (saveGalleryButton)
            saveGalleryButton.gameObject.SetActive(rank >= Rank.SPlus);
    }

    void OnDeliver()
    {
        if (GameManager.I == null || GameManager.I.currentOrder == null)
        {
            Close();
            return;
        }

        var order = GameManager.I.currentOrder;
        int payment = GameManager.I.ComputePayment(order.minPayment, order.maxPayment, rank);
        GameManager.I.CompleteOrder(rank, payment);
        Close();
    }

    void OnSaveGallery()
    {
        if (GameManager.I == null || GameManager.I.currentOrder == null)
        {
            Close();
            return;
        }

        var order = GameManager.I.currentOrder;
        int payment = GameManager.I.ComputePayment(order.minPayment, order.maxPayment, rank);
        LegendaryWeaponsGallery.I?.Add(spec, rank, payment);
        GameManager.I.CompleteOrder(rank, payment);
        Close();
    }

    void Close()
    {
        Destroy(gameObject);
    }
}

