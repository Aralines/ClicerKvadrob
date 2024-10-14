using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonWithPreview : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Button mainButton;               // ������, �� ������� ���������� ���������
    public Image previewImage;              // �������� � ����������� ������������
    public ResourceManager resourceManager; // ������ �� ResourceManager
    public double requiredResources = 100;  // ��������� ���������� �������� ��� ��������� �����
    public float scaleFactor = 0.9f;        // ����������� ���������� ������
    public float animationDuration = 0.1f;  // ������������ �������� ����������/����������

    private Vector3 originalScale;          // �������� ������ ������
    private bool isImageChanged = false;    // ���� ��� ������������, �������� �� �����������

    void Start()
    {
        // ��������� �������� ������ ������
        if (mainButton != null)
        {
            originalScale = mainButton.transform.localScale;
        }

        // ������������� previewImage � �������� ��������� � ������ ������
        if (previewImage != null)
        {
            previewImage.color = Color.black; // ���������� ������ ����������� ������
        }
    }

    // �����, ���������� ��� ������� �� ������
    public void OnPointerDown(PointerEventData eventData)
    {
        // ���������, ���������� �� �������� � �� �������� �� ��� �����������
        if (!isImageChanged && resourceManager != null && resourceManager.GetResources() >= requiredResources)
        {
            previewImage.color = Color.white; // ������ ���� ����������� �� �����
            isImageChanged = true;  // ������������� ����, ����� ������ �� ������ ����
            Debug.Log("���� ����������� ������� �� �����. ���������� ��������: " + resourceManager.GetResources());
        }

        // �������� �������� ���������� ������
        StopAllCoroutines();
        StartCoroutine(ScaleButton(originalScale * scaleFactor));
    }

    // �����, ���������� ��� ���������� ������
    public void OnPointerUp(PointerEventData eventData)
    {
        // �������� �������� �������� ������ � ��������� �������
        StopAllCoroutines();
        StartCoroutine(ScaleButton(originalScale));
    }

    // ������� ��� �������� ��������� ������� ������
    private IEnumerator ScaleButton(Vector3 targetScale)
    {
        Vector3 startScale = mainButton.transform.localScale;
        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            mainButton.transform.localScale = Vector3.Lerp(startScale, targetScale, elapsed / animationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mainButton.transform.localScale = targetScale;
    }

    // ����� ��� ��������� ������� �������� ������, �� ������������� previewImage
    public void ChangeMainButtonSprite(Sprite newSprite)
    {
        if (mainButton != null && newSprite != null)
        {
            mainButton.image.sprite = newSprite;
            Debug.Log("������ �������� ������ �������.");
        }
    }
}