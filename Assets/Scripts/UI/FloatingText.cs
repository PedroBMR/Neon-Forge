using UnityEngine;
using TMPro;
using System.Collections;

public class FloatingText : MonoBehaviour
{
    TextMeshProUGUI text;
    RectTransform rect;

    [Tooltip("Pixels per second the text moves upward")] public float speed = 50f;
    [Tooltip("Seconds before the text disappears")] public float lifetime = 1f;

    public void Show(string message, Vector3 position, Color? color = null)
    {
        if (!rect) rect = GetComponent<RectTransform>();
        if (!text)
        {
            text = gameObject.AddComponent<TextMeshProUGUI>();
            text.alignment = TextAlignmentOptions.Center;
        }

        rect.position = position;
        text.text = message;
        if (color.HasValue) text.color = color.Value;

        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        float t = 0f;
        Color start = text.color;
        while (t < lifetime)
        {
            float dt = Time.deltaTime;
            t += dt;
            rect.position += Vector3.up * speed * dt;
            text.color = new Color(start.r, start.g, start.b, 1f - t / lifetime);
            yield return null;
        }
        Destroy(gameObject);
    }
}

