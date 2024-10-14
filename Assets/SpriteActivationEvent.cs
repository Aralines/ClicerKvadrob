using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpriteActivationEvent : MonoBehaviour
{
    [SerializeField] private Image buttonImage;             // Компонент Image для отслеживания изменения спрайта на кнопке
    [SerializeField] private Sprite targetSprite;           // Спрайт, при котором объект будет активирован
    [SerializeField] private GameObject targetObject;       // Объект, который будет активирован
    [SerializeField] private UnityEvent onSpriteMatched;    // Событие, которое будет вызвано при совпадении спрайта

    private void Start()
    {
        // Убедимся, что ссылки установлены корректно
        if (buttonImage == null || targetSprite == null || targetObject == null)
        {
            Debug.LogError("Не все ссылки установлены.");
            return;
        }

        // Деактивируем объект по умолчанию
        targetObject.SetActive(false);
    }

    private void Update()
    {
        // Проверяем, совпадает ли текущий спрайт с целевым спрайтом
        if (buttonImage.sprite == targetSprite)
        {
            // Активируем объект и вызываем событие
            targetObject.SetActive(true);
            onSpriteMatched?.Invoke();
        }
    }
}
