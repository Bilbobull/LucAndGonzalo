using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMeterUI : MonoBehaviour {

    public AbilityMeter meter;
    public Vector2 offset;
    public float FadeRate;
    RectTransform rect;
    float originalWidth;

    public bool invert;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        originalWidth = rect.rect.width;
        Transform canvas = GameObject.Find("Canvas").transform;
        transform.SetParent(canvas, false);
    }

    // Update is called once per frame
    void Update ()
    {
        Vector2 targetPos = Camera.main.WorldToViewportPoint(meter.transform.position);
        targetPos += offset;

        rect.anchorMin = targetPos;
        rect.anchorMax = targetPos;

        Scale();
        Fade();
    }

    void Fade()
    {
        Color c = GetComponent<Image>().color;
        // Be invisible if nothing interesting is happening
        if (!meter.IsCharging && (meter.IsEmpty || meter.IsFull))
        {
            // Fade out
            c.a = Mathf.Lerp(c.a, 0, FadeRate);
        }
        else
        {
            // Fade in
            c.a = Mathf.Lerp(c.a, 1, FadeRate);
        }
        GetComponent<Image>().color = c;
    }

    void Scale()
    {
        Vector2 newSize = rect.sizeDelta;
        if(invert)
            newSize.x = originalWidth * (1.0f - meter.Amount);
        else
            newSize.x = originalWidth * meter.Amount;

        rect.sizeDelta = newSize;
    }
}
