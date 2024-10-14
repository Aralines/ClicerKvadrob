using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SpriteManager : MonoBehaviour
{
    public Button mainButton;                 // Основная кнопка, в которой будет меняться спрайт
    public List<Sprite> sprites;              // Коллекция спрайтов
    public List<Button> buttons;              // Список кнопок, каждая будет менять спрайт
    public List<int> requiredResources;       // Список, хранящий количество ресурсов для каждой кнопки
    public List<Image> previewImages;         // Список изображений, которые будут окрашиваться в белый цвет
    public ResourceManager resourceManager;   // Ссылка на ResourceManager для проверки ресурсов
    public List<Button> extraButtons;         // Дополнительные кнопки, каждая из которых будет бесплатно менять спрайт на главной кнопке

    private List<bool> hasSpentResources;     // Список флагов для отслеживания, потрачены ли ресурсы для каждой кнопки

    void Start()
    {
        // Проверяем, что у нас есть ссылки на спрайты, основную кнопку, дополнительные кнопки, изображения и количество ресурсов
        if (mainButton == null || sprites == null || buttons == null || resourceManager == null || requiredResources == null || previewImages == null || extraButtons == null)
        {
            Debug.LogError("Не все ссылки установлены.");
            return;
        }

        // Убедимся, что количество спрайтов, кнопок, ресурсов и изображений соответствует
        if (sprites.Count != buttons.Count || requiredResources.Count != buttons.Count || previewImages.Count != buttons.Count)
        {
            Debug.LogError("Количество спрайтов, кнопок, требуемых ресурсов и изображений должно совпадать.");
            return;
        }

        // Инициализируем список флагов, чтобы отслеживать, потрачены ли ресурсы для каждой кнопки
        hasSpentResources = new List<bool>(new bool[buttons.Count]);

        // Загрузка состояния кнопок и главного спрайта из сохранений
        LoadButtonStates();
        LoadMainButtonSprite();

        // Назначаем каждой кнопке действие при нажатии
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i;  // Копируем индекс в локальную переменную, чтобы избежать проблем с замыканием
            buttons[i].onClick.AddListener(() => ChangeMainButtonSprite(index));

            // Устанавливаем состояние previewImages в зависимости от сохраненных данных
            if (previewImages[i] != null)
            {
                previewImages[i].color = hasSpentResources[i] ? Color.white : Color.black;
            }
        }

        // Назначаем каждой дополнительной кнопке действие при нажатии
        foreach (Button extraButton in extraButtons)
        {
            extraButton.onClick.AddListener(() => ChangeMainButtonSpriteToExtra(extraButton));
        }
    }

    // Метод для изменения спрайта основной кнопки
    private void ChangeMainButtonSprite(int spriteIndex)
    {
        // Проверяем, достаточно ли ресурсов для смены спрайта и тратим их только один раз
        if (!hasSpentResources[spriteIndex])
        {
            int requiredResourceAmount = requiredResources[spriteIndex];
            if (resourceManager.GetResources() >= requiredResourceAmount)
            {
                if (resourceManager.SpendResources(requiredResourceAmount))
                {
                    hasSpentResources[spriteIndex] = true; // Устанавливаем флаг, чтобы не тратить ресурсы повторно для этой кнопки
                    ApplySpriteChange(spriteIndex);
                    ChangePreviewImageToWhite(spriteIndex); // Изменяем цвет изображения на белый только если ресурсы успешно потрачены
                    SaveButtonStates(); // Сохранение состояния кнопок
                    SaveMainButtonSprite(spriteIndex); // Сохранение состояния главного спрайта
                    YandexGame.savesData.extraButtonSpriteName = ""; // Сбрасываем сохраненный спрайт из extraButtons
                    YandexGame.SaveProgress();
                    Debug.Log($"Ресурсы потрачены, спрайт основной кнопки изменен на: {sprites[spriteIndex].name}");
                }
                else
                {
                    Debug.LogWarning("Не удалось потратить ресурсы.");
                }
            }
            else
            {
                Debug.LogWarning($"Недостаточно ресурсов для изменения спрайта. Требуется: {requiredResourceAmount}");
            }
        }
        else
        {
            // Если ресурсы уже были потрачены, просто применяем изменение спрайта без траты ресурсов
            ApplySpriteChange(spriteIndex);
            SaveMainButtonSprite(spriteIndex); // Сохранение состояния главного спрайта
            YandexGame.savesData.extraButtonSpriteName = ""; // Сбрасываем сохраненный спрайт из extraButtons
            YandexGame.SaveProgress();
        }
    }

    // Метод для изменения спрайта основной кнопки на спрайт, который находится на дополнительной кнопке
    private void ChangeMainButtonSpriteToExtra(Button extraButton)
    {
        Sprite newSprite = extraButton.image.sprite;

        if (newSprite != null)
        {
            // Устанавливаем новый спрайт на основную кнопку
            mainButton.image.sprite = newSprite;

            // Сохраняем имя спрайта, чтобы позже можно было найти его
            YandexGame.savesData.extraButtonSpriteName = newSprite.name;
            YandexGame.savesData.mainButtonSpriteIndex = -1; // Сбрасываем индекс основного спрайта
            YandexGame.SaveProgress();

            Debug.Log("Спрайт основной кнопки изменен на спрайт дополнительной кнопки: " + newSprite.name);
        }
    }

    // Метод для применения изменения спрайта
    private void ApplySpriteChange(int spriteIndex)
    {
        if (spriteIndex >= 0 && spriteIndex < sprites.Count)
        {
            mainButton.image.sprite = sprites[spriteIndex];
        }
        else
        {
            Debug.LogWarning("Индекс спрайта за пределами диапазона.");
        }
    }

    // Метод для изменения цвета конкретного previewImage на белый
    private void ChangePreviewImageToWhite(int spriteIndex)
    {
        if (previewImages[spriteIndex] != null)
        {
            previewImages[spriteIndex].color = Color.white;
            Debug.Log("Изображение окрашено в белый цвет для индекса: " + spriteIndex);
        }
    }

    // Метод для сохранения состояния кнопок
    private void SaveButtonStates()
    {
        YandexGame.savesData.buttonClickedStatus = hasSpentResources.ToArray();
        YandexGame.SaveProgress();
    }

    // Метод для загрузки состояния кнопок
    private void LoadButtonStates()
    {
        if (YandexGame.savesData.buttonClickedStatus != null && YandexGame.savesData.buttonClickedStatus.Length == buttons.Count)
        {
            hasSpentResources = new List<bool>(YandexGame.savesData.buttonClickedStatus);
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
        if (YandexGame.savesData.mainButtonSpriteIndex >= 0 && YandexGame.savesData.mainButtonSpriteIndex < sprites.Count)
        {
            mainButton.image.sprite = sprites[YandexGame.savesData.mainButtonSpriteIndex];
        }
        else if (!string.IsNullOrEmpty(YandexGame.savesData.extraButtonSpriteName))
        {
            // Ищем спрайт по имени среди extraButtons
            foreach (Button extraButton in extraButtons)
            {
                if (extraButton.image.sprite != null && extraButton.image.sprite.name == YandexGame.savesData.extraButtonSpriteName)
                {
                    mainButton.image.sprite = extraButton.image.sprite;
                    break;
                }
            }
        }
    }
}