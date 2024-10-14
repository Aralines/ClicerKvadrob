using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class AutoClickManager : MonoBehaviour
{
    public ResourceManager resourceManager;     // Ссылка на ResourceManager
    public TextMeshProUGUI autoClickPowerText;  // Текст для отображения силы автосбора
    public List<double> autoClickCosts;         // Список стоимости автосбора
    public List<double> autoClickPowers;        // Список силы автосбора
    public float autoClickInterval = 1.0f;      // Интервал автосбора в секундах
    private double autoClickPower = 0;          // Текущая мощность автосбора
    private bool autoClickEnabled = false;      // Статус автосбора

    void Start()
    {
        LoadAutoClickPower(); // Загрузка сохраненной мощности автосбора
        UpdateAutoClickPowerText();
    }

    // Метод для покупки автосбора по индексу
    public void BuyAutoClick(int index)
    {
        if (index >= 0 && index < autoClickCosts.Count && index < autoClickPowers.Count)
        {
            double cost = autoClickCosts[index];
            double power = autoClickPowers[index];

            // Проверяем, хватает ли ресурсов для покупки автосбора
            if (resourceManager.SpendResources(cost))
            {
                autoClickPower += power;
                UpdateAutoClickPowerText();
                SaveAutoClickPower(); // Сохранение мощности автосбора после покупки

                // Включаем автоклики, если они еще не были включены
                if (!autoClickEnabled)
                {
                    autoClickEnabled = true;
                    InvokeRepeating(nameof(PerformAutoClick), autoClickInterval, autoClickInterval);
                }
            }
        }
    }

    // Метод для выполнения автосбора
    private void PerformAutoClick()
    {
        resourceManager.AddResources(autoClickPower);
    }

    // Метод для обновления текста силы автосбора
    private void UpdateAutoClickPowerText()
    {
        if (autoClickPowerText != null)
        {
            autoClickPowerText.text = $" {autoClickPower:F2}";
        }
    }

    // Метод для получения текущей силы автосбора (если нужно)
    public double GetAutoClickPower()
    {
        return autoClickPower;
    }

    // Метод для увеличения мощности автосбора
    public void IncreaseAutoClickPower(double amount)
    {
        autoClickPower *= amount;
        UpdateAutoClickPowerText();
        SaveAutoClickPower(); // Сохранение мощности автосбора после увеличения
    }

    // Метод для сохранения текущей мощности автосбора
    private void SaveAutoClickPower()
    {
        YandexGame.savesData.autoClickPower = autoClickPower;
        YandexGame.SaveProgress();
    }

    // Метод для загрузки сохраненной мощности автосбора
    private void LoadAutoClickPower()
    {
        if (YandexGame.savesData.autoClickPower != 0)
        {
            autoClickPower = YandexGame.savesData.autoClickPower;
        }
    }
}