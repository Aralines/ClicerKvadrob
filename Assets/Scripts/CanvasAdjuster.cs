using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasAdjuster : MonoBehaviour
{
    private CanvasScaler canvasScaler;

    void Start()
    {
        canvasScaler = GetComponent<CanvasScaler>();

        if (canvasScaler != null)
        {
            // Определение устройства
            if (Application.isMobilePlatform)
            {
                // Если мобильное устройство, установить Match на 1
                canvasScaler.matchWidthOrHeight = 1.0f;
            }
            else
            {
                // Если компьютер или другое устройство, установить Match на 0.5
                canvasScaler.matchWidthOrHeight = 0f;
            }
        }
    }
}
