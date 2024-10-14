using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public TextMeshProUGUI resourceText;
    public double resources = 0;  // Используем double для работы с большими числами

    // Метод для добавления ресурсов
    public void AddResources(double amount)
    {
        resources += amount;
        resourceText.text = FormatNumber(resources) + " Кристаллы";
    }

    // Метод для трат ресурсов
    public bool SpendResources(double amount)
    {
        if (resources >= amount)
        {
            resources -= amount;
            resourceText.text = FormatNumber(resources) + " Кристаллы";
            return true;
        }
        return false;
    }

    // Новый метод для установки ресурсов
    public void SetResources(double amount)
    {
        resources = amount;
        resourceText.text = FormatNumber(resources) + " Кристаллы";
    }

    // Метод для получения текущего количества ресурсов
    public double GetResources()
    {
        return resources;
    }

    // Функция для форматирования чисел
    public string FormatNumber(double number)
    {
        if (number >= 1000000000000000000) // Квинтиллионы
            return (number / 1000000000000000000D).ToString("0.##") + "aa";
        else if (number >= 1000000000000000) // Квадриллионы
            return (number / 1000000000000000D).ToString("0.##") + "ab";
        else if (number >= 1000000000000) // Триллионы
            return (number / 1000000000000D).ToString("0.##") + "T";
        else if (number >= 1000000000) // Миллиарды
            return (number / 1000000000D).ToString("0.##") + "B";
        else if (number >= 1000000) // Миллионы
            return (number / 1000000D).ToString("0.##") + "M";
        else if (number >= 1000) // Тысячи
            return (number / 1000D).ToString("0.##") + "K";
        else
            return number.ToString("0");
    }
}
