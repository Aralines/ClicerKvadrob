using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeClickEffect : MonoBehaviour
{
    public Button treeButton;  // ������ �� ������ � ������������ ������
    public RectTransform treeRectTransform;  // ������ �� RectTransform ������
    public float scaleFactor = 0.9f;  // ����������� ����������
    public float animationDuration = 0.2f;  // ������������ ��������

    private Vector3 originalScale;  // ������������ ������

    void Start()
    {
        // ��������� �������� ������
        originalScale = treeRectTransform.localScale;

        // ����������� ������� � ������� ������� �� ������
        treeButton.onClick.AddListener(OnTreeClicked);
    }

    // �����, ������� ���������� ��� ������� �� ������
    void OnTreeClicked()
    {
        // ��������� �������� ��� ���������� � �������������� ��������
        StartCoroutine(AnimateScale());
    }

    // �������� ��� �������� ��������� ��������
    IEnumerator AnimateScale()
    {
        // ��������� ������
        yield return ScaleOverTime(treeRectTransform, originalScale * scaleFactor, animationDuration / 2);

        // ��������������� �������� ������ ������
        yield return ScaleOverTime(treeRectTransform, originalScale, animationDuration / 2);
    }

    // ����� ��� �������� ��������� �������� �������
    IEnumerator ScaleOverTime(RectTransform target, Vector3 targetScale, float duration)
    {
        Vector3 initialScale = target.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            target.localScale = Vector3.Lerp(initialScale, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // ������������, ��� �������� ������� ����� ��������� � ��������
        target.localScale = targetScale;
    }
}
