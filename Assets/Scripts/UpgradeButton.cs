using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class UpgradeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float scaleFactor = 0.9f;        // Коэффициент уменьшения (например, 0.9 для уменьшения на 10%)
    public float animationDuration = 0.1f;  // Длительность анимации уменьшения/увеличения

    private Vector3 originalScale;          // Исходный размер кнопки

    void Start()
    {
        // Сохраняем исходный размер кнопки
        originalScale = transform.localScale;
    }

    // Метод, вызываемый при нажатии на кнопку
    public void OnPointerDown(PointerEventData eventData)
    {
        // Запускаем корутину для уменьшения кнопки
        StopAllCoroutines();
        StartCoroutine(ScaleButton(originalScale * scaleFactor));
    }

    // Метод, вызываемый при отпускании кнопки
    public void OnPointerUp(PointerEventData eventData)
    {
        // Запускаем корутину для восстановления размера кнопки
        StopAllCoroutines();
        StartCoroutine(ScaleButton(originalScale));
    }

    // Корутин для плавного изменения размера кнопки
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

        transform.localScale = targetScale; // Устанавливаем точный конечный размер
    }
}