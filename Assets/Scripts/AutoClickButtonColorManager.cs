using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoClickButtonColorManager : MonoBehaviour
{
    public AutoClickManager autoClickManager; // Ссылка на AutoClickManager для проверки ресурсов
    public List<Button> upgradeButtons; // Список кнопок для улучшений
    public Color defaultColor; // Основной цвет кнопки
    public Color clickedColor; // Цвет кнопки после нажатия

    void Start()
    {
        // Привязка методов к кнопкам
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            int index = i; // Локальная переменная для предотвращения проблем с замыканием
            upgradeButtons[i].onClick.AddListener(() => OnButtonClick(index));
            upgradeButtons[i].GetComponent<Image>().color = defaultColor; // Установка начального цвета кнопки
        }
    }

    void OnButtonClick(int index)
    {
        if (index >= 0 && index < autoClickManager.autoClickCosts.Count)
        {
            double cost = autoClickManager.autoClickCosts[index];
            if (HasSufficientResources(cost))
            {
                autoClickManager.BuyAutoClick(index); // Покупка автоклика
                upgradeButtons[index].GetComponent<Image>().color = clickedColor; // Изменение цвета кнопки после нажатия, если хватает ресурсов
            }
        }
    }

    bool HasSufficientResources(double cost)
    {
        return autoClickManager.resourceManager.GetResources() >= cost;
    }
}



