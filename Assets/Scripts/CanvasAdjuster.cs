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
            // ����������� ����������
            if (Application.isMobilePlatform)
            {
                // ���� ��������� ����������, ���������� Match �� 1
                canvasScaler.matchWidthOrHeight = 1.0f;
            }
            else
            {
                // ���� ��������� ��� ������ ����������, ���������� Match �� 0.5
                canvasScaler.matchWidthOrHeight = 0f;
            }
        }
    }
}
