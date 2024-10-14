using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalClickEffect : MonoBehaviour
{
    public GameObject crystalPrefab;  // ������ ���������
    public Transform canvas;  // Canvas ��� ���������� ����������
    public Button clickButton;  // ������ �� ������

    void Start()
    {
        // ����������� ������� � ������� ������� �� ������
        clickButton.onClick.AddListener(SpawnCrystalAtMousePosition);
    }

    void SpawnCrystalAtMousePosition()
    {
        // �������� ������� �������
        Vector3 mousePosition = Input.mousePosition;

        // ������ �������� �� ������� �������
        GameObject newCrystal = Instantiate(crystalPrefab, canvas);
        newCrystal.transform.position = mousePosition;

        // ��������� ����������� ����� �� ���������
        DisableRaycastTarget(newCrystal);

        // ��������� �������� ���������
        StartCoroutine(MoveAndFadeOut(newCrystal));
    }

    void DisableRaycastTarget(GameObject crystal)
    {
        // ��������� Raycast Target �� ���� UI-��������� ���������
        Image crystalImage = crystal.GetComponent<Image>();
        if (crystalImage != null)
        {
            crystalImage.raycastTarget = false;  // ��������� �����
        }
    }

    IEnumerator MoveAndFadeOut(GameObject crystal)
    {
        float duration = 1.5f;  // ������������ ��������
        float elapsed = 0f;
        CanvasGroup canvasGroup = crystal.AddComponent<CanvasGroup>();  // ��������� CanvasGroup ��� ������������

        Vector3 startPos = crystal.transform.position;
        Vector3 endPos = startPos + new Vector3(0, 50, 0);  // ��������� �������� �� 50 �������� �����

        while (elapsed < duration)
        {
            // ������� ����������� ��������� �����
            crystal.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);

            // ������� ���������� ������������
            canvasGroup.alpha = 1 - (elapsed / duration);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // ���������� �������� ����� ���������� ��������
        Destroy(crystal);
    }
}
