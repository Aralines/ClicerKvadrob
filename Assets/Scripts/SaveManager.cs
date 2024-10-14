using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public TextMeshProUGUI resourceText;
    public double resources = 0;  // ���������� double ��� ������ � �������� �������

    // ����� ��� ���������� ��������
    public void AddResources(double amount)
    {
        resources += amount;
        resourceText.text = FormatNumber(resources) + " ���������";
    }

    // ����� ��� ���� ��������
    public bool SpendResources(double amount)
    {
        if (resources >= amount)
        {
            resources -= amount;
            resourceText.text = FormatNumber(resources) + " ���������";
            return true;
        }
        return false;
    }

    // ����� ����� ��� ��������� ��������
    public void SetResources(double amount)
    {
        resources = amount;
        resourceText.text = FormatNumber(resources) + " ���������";
    }

    // ����� ��� ��������� �������� ���������� ��������
    public double GetResources()
    {
        return resources;
    }

    // ������� ��� �������������� �����
    public string FormatNumber(double number)
    {
        if (number >= 1000000000000000000) // ������������
            return (number / 1000000000000000000D).ToString("0.##") + "aa";
        else if (number >= 1000000000000000) // ������������
            return (number / 1000000000000000D).ToString("0.##") + "ab";
        else if (number >= 1000000000000) // ���������
            return (number / 1000000000000D).ToString("0.##") + "T";
        else if (number >= 1000000000) // ���������
            return (number / 1000000000D).ToString("0.##") + "B";
        else if (number >= 1000000) // ��������
            return (number / 1000000D).ToString("0.##") + "M";
        else if (number >= 1000) // ������
            return (number / 1000D).ToString("0.##") + "K";
        else
            return number.ToString("0");
    }
}
