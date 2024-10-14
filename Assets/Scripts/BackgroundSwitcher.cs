using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSwitcher : MonoBehaviour
{
    public List<Sprite> pcBackgrounds; // ������ ����� ��� ����������
    public List<Sprite> mobileBackgrounds; // ������ ����� ��� ��������� ���������
    public Image backgroundImage; // ��������� Image, ������������ ���

    private List<Sprite> currentBackgrounds; // ������� ������ ����� ��� �����
    private int currentIndex = 0;

    void Start()
    {
        // ����������� ����������
        if (Application.isMobilePlatform)
        {
            currentBackgrounds = mobileBackgrounds;
            AdjustBackgroundSizeForMobile();
        }
        else
        {
            currentBackgrounds = pcBackgrounds;
        }

        if (currentBackgrounds.Count > 0)
        {
            StartCoroutine(ChangeBackground());
        }
    }

    IEnumerator ChangeBackground()
    {
        while (true)
        {
            // ������������� ������� ���
            backgroundImage.sprite = currentBackgrounds[currentIndex];

            // ��������� � ���������� ����
            currentIndex = (currentIndex + 1) % currentBackgrounds.Count;

            // ���� 30 ������ ����� ������ ����
            yield return new WaitForSeconds(30f);
        }
    }

    void AdjustBackgroundSizeForMobile()
    {
        if (backgroundImage != null)
        {
            // �������� RectTransform ��� ��������� ������� UI-��������
            RectTransform rectTransform = backgroundImage.GetComponent<RectTransform>();

            // ��������, ����� ���������, ��� RectTransform ������
            if (rectTransform != null)
            {
                // ������������� ����� ������ ����������� ��� ���������� ���������� ��� ��������� ���������
                rectTransform.sizeDelta = new Vector2(720, 1280);
                Debug.Log("������ ���� ���������� �� 720x1280 ��� ���������� ����������.");
            }
            else
            {
                Debug.LogWarning("RectTransform ��� backgroundImage �� ������.");
            }
        }
        else
        {
            Debug.LogError("�� �������� ������ backgroundImage � ����������.");
        }
    }
}
