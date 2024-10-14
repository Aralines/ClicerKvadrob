using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class ResourceManager : MonoBehaviour
{
    public TextMeshProUGUI resourceText;   
    public double resources = 0;         

    void Start()
    {
        LoadResources(); 
        UpdateResourceText();
        YandexGame.FullscreenShow();
    }

    // Метод для добавления ресурсов
    public void AddResources(double amount)
    {
       
        resources += amount;
        UpdateResourceText();
        SaveResources(); 
    }

    // Метод для трат ресурсов
    public bool SpendResources(double amount)
    {
        YandexGame.FullscreenShow();
        if (resources >= amount)
        {
            resources -= amount;
            UpdateResourceText();
            SaveResources();
            return true;
        }
        return false;
    }

    // Метод для получения текущего количества ресурсов
    public double GetResources()
    {
        return resources;
    }

    // Метод для обновления текста ресурсов
    private void UpdateResourceText()
    {
        resourceText.text = FormatNumber(resources);
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

    // Метод для сохранения текущего количества ресурсов
    private void SaveResources()
    {
        YandexGame.savesData.resources = resources;
        YandexGame.SaveProgress();
    }

    // Метод для загрузки сохраненных ресурсов
    private void LoadResources()
    {
        if (YandexGame.savesData.resources != 0)
        {
            resources = YandexGame.savesData.resources;
        }
    }
}