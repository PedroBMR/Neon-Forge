using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VIPGalleryPanel : MonoBehaviour
{
    public TextMeshProUGUI countTxt;
    public Button closeButton;
    public Button legendaryButton;

    void Awake()
    {
        if (closeButton != null)
            closeButton.onClick.AddListener(Close);
        if (legendaryButton != null)
            legendaryButton.onClick.AddListener(OpenLegendary);
    }

    void OnEnable()
    {
        if (countTxt != null && VIPGallery.I != null)
            countTxt.text = VIPGallery.I.Count.ToString();
    }

    void OpenLegendary()
    {
        UIController.I?.ShowLegendaryGallery();
        Close();
    }

    void Close()
    {
        Destroy(gameObject);
    }
}

