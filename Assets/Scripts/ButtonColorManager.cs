using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ButtonColorManager : MonoBehaviour
{
    public UpgradeManager upgradeManager; // Ссылка на UpgradeManager для проверки ресурсов
    public List<Button> upgradeButtons; // Список кнопок для улучшений
    public Color defaultColor; // Основной цвет кнопки
    public Color clickedColor; // Цвет кнопки после нажатия

    void Start()
    {
        // Инициализация статуса кнопок из сохраненных данных
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            int index = i; // Локальная переменная для предотвращения проблем с замыканием
            upgradeButtons[i].onClick.AddListener(() => OnButtonClick(index));

            // Загрузка состояния кнопок из сохранений
            if (YandexGame.savesData.buttonClickedStatus != null && i < YandexGame.savesData.buttonClickedStatus.Length)
            {
                bool isClicked = YandexGame.savesData.buttonClickedStatus[i];
                upgradeButtons[i].GetComponent<Image>().color = isClicked ? clickedColor : defaultColor;
            }
            else
            {
                upgradeButtons[i].GetComponent<Image>().color = defaultColor;
            }
        }
    }

    void OnButtonClick(int index)
    {
        double cost = upgradeManager.resourceCosts[index];
        if (HasSufficientResources(cost))
        {
            upgradeButtons[index].GetComponent<Image>().color = clickedColor; // Изменение цвета кнопки после нажатия, если хватает ресурсов

            // Сохранение статуса кнопки
            if (YandexGame.savesData.buttonClickedStatus == null || YandexGame.savesData.buttonClickedStatus.Length < upgradeButtons.Count)
            {
                YandexGame.savesData.buttonClickedStatus = new bool[upgradeButtons.Count];
            }
            YandexGame.savesData.buttonClickedStatus[index] = true;

            // Сохранение игры
            YandexGame.SaveProgress();
        }
    }

    bool HasSufficientResources(double cost)
    {
        return upgradeManager.resourceManager.GetResources() >= cost;
    }
}