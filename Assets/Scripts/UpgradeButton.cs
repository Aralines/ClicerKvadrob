using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class UpgradeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float scaleFactor = 0.9f;        // ����������� ���������� (��������, 0.9 ��� ���������� �� 10%)
    public float animationDuration = 0.1f;  // ������������ �������� ����������/����������

    private Vector3 originalScale;          // �������� ������ ������

    void Start()
    {
        // ��������� �������� ������ ������
        originalScale = transform.localScale;
    }

    // �����, ���������� ��� ������� �� ������
    public void OnPointerDown(PointerEventData eventData)
    {
        // ��������� �������� ��� ���������� ������
        StopAllCoroutines();
        StartCoroutine(ScaleButton(originalScale * scaleFactor));
    }

    // �����, ���������� ��� ���������� ������
    public void OnPointerUp(PointerEventData eventData)
    {
        // ��������� �������� ��� �������������� ������� ������
        StopAllCoroutines();
        StartCoroutine(ScaleButton(originalScale));
    }

    // ������� ��� �������� ��������� ������� ������
    private IEnumerator ScaleButton(Vector3 targetScale)
    {
        Vector3 startScale = transform.localScale;
        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsed / animationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale; // ������������� ������ �������� ������
    }
}