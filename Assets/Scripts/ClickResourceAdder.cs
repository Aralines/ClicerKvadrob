using UnityEngine;
using TMPro;
using YG;

public class ClickResourceAdder : MonoBehaviour
{
    public ResourceManager resourceManager;  // Ссылка на ResourceManager для работы с ресурсами
    public TextMeshProUGUI gainAmountText;   // Текст для отображения текущего gainAmount
    public double gainAmount = 1;            // Количество ресурсов, которое добавляется за клик

    void Start()
    {
        LoadGainAmount(); // Загрузка сохраненного gainAmount
        UpdateGainAmountText();
    }

    // Метод для добавления ресурсов через ResourceManager
    public void CallAddClickResources()
    {
        if (resourceManager != null)
        {
            resourceManager.AddResources(gainAmount);
            Debug.Log($"Добавлено {gainAmount} ресурсов.");
        }
        else
        {
            Debug.LogError("ResourceManager не назначен.");
        }
    }

    // Метод для обновления текста gainAmount
    private void UpdateGainAmountText()
    {
        if (gainAmountText != null)
        {
            gainAmountText.text = $"{gainAmount:F2}";
        }
    }

    // Метод для увеличения gainAmount и обновления текста
    public void IncreaseGainAmount(double amount)
    {
        gainAmount += amount;
        UpdateGainAmountText();
        SaveGainAmount(); // Сохранение gainAmount после увеличения
    }

    // Метод для сохранения gainAmount
    private void SaveGainAmount()
    {
        YandexGame.savesData.gainAmount = gainAmount;
        YandexGame.SaveProgress();
    }

    // Метод для загрузки сохраненного gainAmount
    private void LoadGainAmount()
    {
        if (YandexGame.savesData.gainAmount != 0)
        {
            gainAmount = YandexGame.savesData.gainAmount;
        }
    }

    public string FormatNumber(double number)
    {
        if (number >= 1e18) return (number / 1e18).ToString("0.##") + "aa";
        if (number >= 1e15) return (number / 1e15).ToString("0.##") + "ab";
        if (number >= 1e12) return (number / 1e12).ToString("0.##") + "T";
        if (number >= 1e9) return (number / 1e9).ToString("0.##") + "B";
        if (number >= 1e6) return (number / 1e6).ToString("0.##") + "M";
        if (number >= 1e3) return (number / 1e3).ToString("0.##") + "K";
        return number.ToString("0");
    }
}