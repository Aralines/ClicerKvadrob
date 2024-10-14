using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public ResourceManager resourceManager;            // Ссылка на ResourceManager для работы с ресурсами
    public ClickResourceAdder clickResourceAdder;      // Ссылка на ClickResourceAdder для изменения gainAmount
    public List<double> resourceCosts;                 // Список стоимости улучшений в ресурсах
    public List<double> clickPowerIncreases;           // Список прироста ресурсов за клик
    public List<Button> upgradeButtons;                // Список кнопок для разных улучшений

    void Start()
    {
        // Проверяем наличие ссылок на ResourceManager, ClickResourceAdder и списки
        if (resourceManager == null || clickResourceAdder == null)
        {
            Debug.LogError("Не удалось найти компонент ResourceManager или ClickResourceAdder.");
            return;
        }

        // Проверяем, что количество кнопок соответствует количеству улучшений
        if (resourceCosts.Count != clickPowerIncreases.Count || resourceCosts.Count != upgradeButtons.Count)
        {
            Debug.LogError("Количество элементов в списках resourceCosts, clickPowerIncreases и upgradeButtons должно совпадать.");
            return;
        }

        // Привязываем метод к событию OnClick каждой кнопки
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            int index = i; // Локальная копия переменной i, чтобы избежать проблем с замыканием
            upgradeButtons[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    // Метод, который вызывается при нажатии на кнопку
    private void OnButtonClick(int index)
    {
        // Проверяем, что индекс в пределах списка
        if (index < 0 || index >= resourceCosts.Count)
        {
            Debug.LogError("Индекс вне диапазона списка.");
            return;
        }

        // Получаем стоимость улучшения и прирост клика по индексу
        double cost = resourceCosts[index];
        double increase = clickPowerIncreases[index];

        // Проверяем, хватает ли ресурсов для покупки улучшения
        if (resourceManager.SpendResources(cost))
        {
            // Увеличиваем gainAmount у ClickResourceAdder
            clickResourceAdder.IncreaseGainAmount(increase);
            Debug.Log($"Куплено улучшение за {cost} ресурсов! Добавлено {increase} к силе кликов.");
        }
        else
        {
            Debug.Log("Недостаточно ресурсов для покупки улучшения.");
        }
    }
}
