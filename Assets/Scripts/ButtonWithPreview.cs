using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonWithPreview : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Button mainButton;               // Кнопка, на которой происходят изменения
    public Image previewImage;              // Картинка с затемненным изображением
    public ResourceManager resourceManager; // Ссылка на ResourceManager
    public double requiredResources = 100;  // Требуемое количество ресурсов для изменения цвета
    public float scaleFactor = 0.9f;        // Коэффициент уменьшения кнопки
    public float animationDuration = 0.1f;  // Длительность анимации уменьшения/увеличения

    private Vector3 originalScale;          // Исходный размер кнопки
    private bool isImageChanged = false;    // Флаг для отслеживания, изменено ли изображение

    void Start()
    {
        // Сохраняем исходный размер кнопки
        if (mainButton != null)
        {
            originalScale = mainButton.transform.localScale;
        }

        // Устанавливаем previewImage в активное состояние с черным цветом
        if (previewImage != null)
        {
            previewImage.color = Color.black; // Изначально делаем изображение черным
        }
    }

    // Метод, вызываемый при нажатии на кнопку
    public void OnPointerDown(PointerEventData eventData)
    {
        // Проверяем, достаточно ли ресурсов и не изменено ли уже изображение
        if (!isImageChanged && resourceManager != null && resourceManager.GetResources() >= requiredResources)
        {
            previewImage.color = Color.white; // Меняем цвет изображения на белый
            isImageChanged = true;  // Устанавливаем флаг, чтобы больше не менять цвет
            Debug.Log("Цвет изображения изменен на белый. Достаточно ресурсов: " + resourceManager.GetResources());
        }

        // Начинаем анимацию уменьшения кнопки
        StopAllCoroutines();
        StartCoroutine(ScaleButton(originalScale * scaleFactor));
    }

    // Метод, вызываемый при отпускании кнопки
    public void OnPointerUp(PointerEventData eventData)
    {
        // Начинаем анимацию возврата кнопки к исходному размеру
        StopAllCoroutines();
        StartCoroutine(ScaleButton(originalScale));
    }

    // Корутин для плавного изменения размера кнопки
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

    // Метод для изменения спрайта основной кнопки, не затрагивающий previewImage
    public void ChangeMainButtonSprite(Sprite newSprite)
    {
        if (mainButton != null && newSprite != null)
        {
            mainButton.image.sprite = newSprite;
            Debug.Log("Спрайт основной кнопки изменен.");
        }
    }
}