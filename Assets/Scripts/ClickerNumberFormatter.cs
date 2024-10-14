using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickerNumberFormatter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro; // ��������� ��������� ��� ����������� �����
    [SerializeField] private double number;               // �����, ������� ����� ���������������

    private void Update()
    {
        // ��������� ��������� �������� �� ������ ����
        textMeshPro.text = FormatNumber(number);
    }

    // ����� ��� �������������� ����� � ����� "1K", "1M", "1B" � �.�.
    private string FormatNumber(double value)
    {
        if (value < 1000)
        {
            return value.ToString("F0"); // ���������� ����� ��� ���������� ������, ���� ������ 1000
        }
        else if (value < 1_000_000)
        {
            return (value / 1000).ToString("F1") + "K"; // ������ (K)
        }
        else if (value < 1_000_000_000)
        {
            return (value / 1_000_000).ToString("F1") + "M"; // �������� (M)
        }
        else if (value < 1_000_000_000_000)
        {
            return (value / 1_000_000_000).ToString("F1") + "B"; // ��������� (B)
        }
        else if (value < 1e15)
        {
            return (value / 1e12).ToString("F1") + "T"; // ��������� (T)
        }
        else if (value < 1e18)
        {
            return (value / 1e15).ToString("F1") + "Qa"; // ������������ (Qa)
        }
        else if (value < 1e21)
        {
            return (value / 1e18).ToString("F1") + "Qi"; // ������������ (Qi)
        }
        else
        {
            return value.ToString("0.#e+0"); // ���������� ���������������� ������ ��� ����� ������� �����
        }
    }
}
