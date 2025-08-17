using UnityEngine;
using UnityEngine.EventSystems;

public sealed class TapController : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData e)
    {
        Debug.Log("[Tap] pointer down");            // <-- teste
        if (GameManager.I != null) GameManager.I.HammerTap();
    }
}
