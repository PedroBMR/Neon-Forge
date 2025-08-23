using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LegendaryGalleryPanel : MonoBehaviour
{
    public TextMeshProUGUI listTxt;
    public Button closeButton;
    public Button vipButton;

    void Awake()
    {
        if (closeButton != null)
            closeButton.onClick.AddListener(Close);
        if (vipButton != null)
            vipButton.onClick.AddListener(OpenVIP);
    }

    void OnEnable()
    {
        if (listTxt != null && LegendaryWeaponsGallery.I != null)
        {
            var sb = new StringBuilder();
            int index = 1;
            foreach (var entry in LegendaryWeaponsGallery.I.Entries)
            {
                sb.AppendLine($"{index}. {entry.spec.weaponType} ({entry.rank}) - {entry.value:0}");
                index++;
            }
            listTxt.text = sb.ToString();
        }
    }

    void OpenVIP()
    {
        UIController.I?.ShowVIPGallery();
        Close();
    }

    void Close()
    {
        Destroy(gameObject);
    }
}

