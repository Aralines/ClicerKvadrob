using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using YG;

public class ButtonScaler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public Button button;              // Ссылка на кнопку
    public float scaleFactor = 0.9f;   // Коэффициент уменьшения (например, 0.9 для уменьшения на 10%)
     
    public float animationDuration = 0.1f; // Длительность анимации уменьшения/увеличения

    private Vector3 originalScale;     // Исходный размер кнопки

    void Start()
    {
        YandexGame.FullscreenShow();
        // Сохраняем исходный размер кнопки
        if (button != null)
        {
            originalScale = button.transform.localScale;
        }
        else
        {
            Debug.LogError("Кнопка не назначена в инспекторе.");
        }
    }

    // Метод, вызываемый при нажатии на кнопку
    public void OnPointerDown(PointerEventData eventData)
    {
        StopAllCoroutines();  // Останавливаем любые запущенные корутины
        StartCoroutine(ScaleButton(originalScale * scaleFactor));  // Запускаем корутину для уменьшения
    }

    // Метод, вызываемый при отпускании кнопки
    public void OnPointerUp(PointerEventData eventData)
    {
        StopAllCoroutines();  // Останавливаем любые запущенные корутины
        StartCoroutine(ScaleButton(originalScale));  // Запускаем корутину для возвращения к исходному размеру
    }

    

    // Метод, вызываемый при уходе курсора с кнопки
    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();  // Останавливаем любые запущенные корутины
        StartCoroutine(ScaleButton(originalScale));  // Запускаем корутину для возвращения к исходному размеру
    }

    // Корутин для плавного изменения размера кнопки
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

        button.transform.localScale = targetScale; // Обеспечиваем точное совпадение с конечным размером
    }
}