using UnityEngine;
using TMPro;
using YG;

public class ClickResourceAdder : MonoBehaviour
{
    public ResourceManager resourceManager;  // ������ �� ResourceManager ��� ������ � ���������
    public TextMeshProUGUI gainAmountText;   // ����� ��� ����������� �������� gainAmount
    public double gainAmount = 1;            // ���������� ��������, ������� ����������� �� ����

    void Start()
    {
        LoadGainAmount(); // �������� ������������ gainAmount
        UpdateGainAmountText();
    }

    // ����� ��� ���������� �������� ����� ResourceManager
    public void CallAddClickResources()
    {
        if (resourceManager != null)
        {
            resourceManager.AddResources(gainAmount);
            Debug.Log($"��������� {gainAmount} ��������.");
        }
        else
        {
            Debug.LogError("ResourceManager �� ��������.");
        }
    }

    // ����� ��� ���������� ������ gainAmount
    private void UpdateGainAmountText()
    {
        if (gainAmountText != null)
        {
            gainAmountText.text = $"{gainAmount:F2}";
        }
    }

    // ����� ��� ���������� gainAmount � ���������� ������
    public void IncreaseGainAmount(double amount)
    {
        gainAmount += amount;
        UpdateGainAmountText();
        SaveGainAmount(); // ���������� gainAmount ����� ����������
    }

    // ����� ��� ���������� gainAmount
    private void SaveGainAmount()
    {
        YandexGame.savesData.gainAmount = gainAmount;
        YandexGame.SaveProgress();
    }

    // ����� ��� �������� ������������ gainAmount
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