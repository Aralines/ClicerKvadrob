using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeClickEffect : MonoBehaviour
{
    public Button treeButton;  // Ссылка на кнопку с изображением дерева
    public RectTransform treeRectTransform;  // Ссылка на RectTransform дерева
    public float scaleFactor = 0.9f;  // Коэффициент уменьшения
    public float animationDuration = 0.2f;  // Длительность анимации

    private Vector3 originalScale;  // Оригинальный размер

    void Start()
    {
        // Сохраняем исходный размер
        originalScale = treeRectTransform.localScale;

        // Привязываем функцию к событию нажатия на кнопку
        treeButton.onClick.AddListener(OnTreeClicked);
    }

    // Метод, который вызывается при нажатии на кнопку
    void OnTreeClicked()
    {
        // Запускаем корутину для уменьшения и восстановления масштаба
        StartCoroutine(AnimateScale());
    }

    // Корутину для анимации изменения масштаба
    IEnumerator AnimateScale()
    {
        // Уменьшаем дерево
        yield return ScaleOverTime(treeRectTransform, originalScale * scaleFactor, animationDuration / 2);

        // Восстанавливаем исходный размер дерева
        yield return ScaleOverTime(treeRectTransform, originalScale, animationDuration / 2);
    }

    // Метод для плавного изменения масштаба объекта
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

        // Обеспечиваем, что конечный масштаб точно совпадает с заданным
        target.localScale = targetScale;
    }
}
