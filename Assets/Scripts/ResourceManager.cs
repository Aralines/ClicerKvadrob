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

    // ����� ��� ���������� ��������
    public void AddResources(double amount)
    {
       
        resources += amount;
        UpdateResourceText();
        SaveResources(); 
    }

    // ����� ��� ���� ��������
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

    // ����� ��� ��������� �������� ���������� ��������
    public double GetResources()
    {
        return resources;
    }

    // ����� ��� ���������� ������ ��������
    private void UpdateResourceText()
    {
        resourceText.text = FormatNumber(resources);
    }

    // ������� ��� �������������� �����
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

    // ����� ��� ���������� �������� ���������� ��������
    private void SaveResources()
    {
        YandexGame.savesData.resources = resources;
        YandexGame.SaveProgress();
    }

    // ����� ��� �������� ����������� ��������
    private void LoadResources()
    {
        if (YandexGame.savesData.resources != 0)
        {
            resources = YandexGame.savesData.resources;
        }
    }
}