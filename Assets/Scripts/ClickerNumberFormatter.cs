using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickerNumberFormatter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro; // Текстовый компонент для отображения числа
    [SerializeField] private double number;               // Число, которое нужно отформатировать

    private void Update()
    {
        // Обновляем текстовое значение на каждый кадр
        textMeshPro.text = FormatNumber(number);
    }

    // Метод для форматирования числа в стиль "1K", "1M", "1B" и т.д.
    private string FormatNumber(double value)
    {
        if (value < 1000)
        {
            return value.ToString("F0"); // Возвращаем число без десятичных знаков, если меньше 1000
        }
        else if (value < 1_000_000)
        {
            return (value / 1000).ToString("F1") + "K"; // Тысячи (K)
        }
        else if (value < 1_000_000_000)
        {
            return (value / 1_000_000).ToString("F1") + "M"; // Миллионы (M)
        }
        else if (value < 1_000_000_000_000)
        {
            return (value / 1_000_000_000).ToString("F1") + "B"; // Миллиарды (B)
        }
        else if (value < 1e15)
        {
            return (value / 1e12).ToString("F1") + "T"; // Триллионы (T)
        }
        else if (value < 1e18)
        {
            return (value / 1e15).ToString("F1") + "Qa"; // Квадриллионы (Qa)
        }
        else if (value < 1e21)
        {
            return (value / 1e18).ToString("F1") + "Qi"; // Квинтиллионы (Qi)
        }
        else
        {
            return value.ToString("0.#e+0"); // Используем экспоненциальную запись для очень больших чисел
        }
    }
}
