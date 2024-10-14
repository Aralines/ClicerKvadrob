using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using YG;

public class ButtonScaler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public Button button;              // ������ �� ������
    public float scaleFactor = 0.9f;   // ����������� ���������� (��������, 0.9 ��� ���������� �� 10%)
     
    public float animationDuration = 0.1f; // ������������ �������� ����������/����������

    private Vector3 originalScale;     // �������� ������ ������

    void Start()
    {
        YandexGame.FullscreenShow();
        // ��������� �������� ������ ������
        if (button != null)
        {
            originalScale = button.transform.localScale;
        }
        else
        {
            Debug.LogError("������ �� ��������� � ����������.");
        }
    }

    // �����, ���������� ��� ������� �� ������
    public void OnPointerDown(PointerEventData eventData)
    {
        StopAllCoroutines();  // ������������� ����� ���������� ��������
        StartCoroutine(ScaleButton(originalScale * scaleFactor));  // ��������� �������� ��� ����������
    }

    // �����, ���������� ��� ���������� ������
    public void OnPointerUp(PointerEventData eventData)
    {
        StopAllCoroutines();  // ������������� ����� ���������� ��������
        StartCoroutine(ScaleButton(originalScale));  // ��������� �������� ��� ����������� � ��������� �������
    }

    

    // �����, ���������� ��� ����� ������� � ������
    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();  // ������������� ����� ���������� ��������
        StartCoroutine(ScaleButton(originalScale));  // ��������� �������� ��� ����������� � ��������� �������
    }

    // ������� ��� �������� ��������� ������� ������
    private IEnumerator ScaleButton(Vector3 targetScale)
    {
        Vector3 startScale = button.transform.localScale;
        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            button.transform.localScale = Vector3.Lerp(startScale, targetScale, elapsed / animationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        button.transform.localScale = targetScale; // ������������ ������ ���������� � �������� ��������
    }
}