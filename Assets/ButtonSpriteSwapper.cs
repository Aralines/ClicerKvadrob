using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ButtonSpriteSwapper : MonoBehaviour
{
    public Spriteman spriteManager; // Ссылка на SpriteManager для доступа к спрайтам
    public Button mainButton;            // Основная кнопка, на которой будет меняться спрайт
    public List<Button> buttons;         // Список кнопок, каждая кнопка имеет свой спрайт

    void Start()
    {
        if (spriteManager == null || mainButton == null || buttons == null)
        {
            Debug.LogError("Не все ссылки назначены в инспекторе.");
            return;
        }

        // Назначаем каждой кнопке действие при нажатии
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i; // Копируем индекс в локальную переменную, чтобы избежать проблем с замыканием
            buttons[i].onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    private void OnButtonClicked(int buttonIndex)
    {
        if (buttonIndex >= 0 && buttonIndex < spriteManager.sprites.Count)
        {
            // Устанавливаем спрайт на основной кнопке из SpriteManager
            mainButton.image.sprite = spriteManager.sprites[buttonIndex];
            SaveMainButtonSprite(buttonIndex); // Сохраняем состояние главного спрайта
        }
        else
        {
            Debug.LogWarning("Индекс кнопки за пределами диапазона.");
        }
    }

    // Метод для сохранения текущего спрайта главной кнопки
    private void SaveMainButtonSprite(int spriteIndex)
    {
        YandexGame.savesData.mainButtonSpriteIndex = spriteIndex;
        YandexGame.SaveProgress();
    }

    // Метод для загрузки спрайта главной кнопки
    private void LoadMainButtonSprite()
    {
        if (YandexGame.savesData.mainButtonSpriteIndex >= 0 && YandexGame.savesData.mainButtonSpriteIndex < spriteManager.sprites.Count)
        {
            mainButton.image.sprite = spriteManager.sprites[YandexGame.savesData.mainButtonSpriteIndex];
        }
    }
}