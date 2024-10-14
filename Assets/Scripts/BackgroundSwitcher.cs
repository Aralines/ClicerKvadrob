using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSwitcher : MonoBehaviour
{
    public List<Sprite> pcBackgrounds; // Список фонов для компьютера
    public List<Sprite> mobileBackgrounds; // Список фонов для мобильных устройств
    public Image backgroundImage; // Компонент Image, отображающий фон

    private List<Sprite> currentBackgrounds; // Текущий список фонов для смены
    private int currentIndex = 0;

    void Start()
    {
        // Определение устройства
        if (Application.isMobilePlatform)
        {
            currentBackgrounds = mobileBackgrounds;
            AdjustBackgroundSizeForMobile();
        }
        else
        {
            currentBackgrounds = pcBackgrounds;
        }

        if (currentBackgrounds.Count > 0)
        {
            StartCoroutine(ChangeBackground());
        }
    }

    IEnumerator ChangeBackground()
    {
        while (true)
        {
            // Устанавливаем текущий фон
            backgroundImage.sprite = currentBackgrounds[currentIndex];

            // Переходим к следующему фону
            currentIndex = (currentIndex + 1) % currentBackgrounds.Count;

            // Ждем 30 секунд перед сменой фона
            yield return new WaitForSeconds(30f);
        }
    }

    void AdjustBackgroundSizeForMobile()
    {
        if (backgroundImage != null)
        {
            // Получаем RectTransform для изменения размера UI-элемента
            RectTransform rectTransform = backgroundImage.GetComponent<RectTransform>();

            // Проверка, чтобы убедиться, что RectTransform найден
            if (rectTransform != null)
            {
                // Устанавливаем новый размер изображения под конкретное разрешение для мобильных устройств
                rectTransform.sizeDelta = new Vector2(720, 1280);
                Debug.Log("Размер фона установлен на 720x1280 для мобильного устройства.");
            }
            else
            {
                Debug.LogWarning("RectTransform для backgroundImage не найден.");
            }
        }
        else
        {
            Debug.LogError("Не назначен объект backgroundImage в инспекторе.");
        }
    }
}
