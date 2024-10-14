using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FormattedTextDisplay : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // Ссылка на компонент TextMeshProUGUI
    public double number;               // Число, которое нужно отформатировать и отобразить

    void Start()
    {
        UpdateText();
    }

    // Метод для обновления текста с форматированным числом
    public void UpdateText()
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = FormatNumber(number);
        }
        else
        {
            Debug.LogError("TextMeshProUGUI не назначен.");
        }
    }

    // Метод для установки числа и обновления текста
    public void SetNumber(double newNumber)
    {
        number = newNumber;
        UpdateText();
    }

    // Функция для форматирования чисел
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
