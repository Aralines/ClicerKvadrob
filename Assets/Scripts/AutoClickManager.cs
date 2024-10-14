using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class AutoClickManager : MonoBehaviour
{
    public ResourceManager resourceManager;     // ������ �� ResourceManager
    public TextMeshProUGUI autoClickPowerText;  // ����� ��� ����������� ���� ���������
    public List<double> autoClickCosts;         // ������ ��������� ���������
    public List<double> autoClickPowers;        // ������ ���� ���������
    public float autoClickInterval = 1.0f;      // �������� ��������� � ��������
    private double autoClickPower = 0;          // ������� �������� ���������
    private bool autoClickEnabled = false;      // ������ ���������

    void Start()
    {
        LoadAutoClickPower(); // �������� ����������� �������� ���������
        UpdateAutoClickPowerText();
    }

    // ����� ��� ������� ��������� �� �������
    public void BuyAutoClick(int index)
    {
        if (index >= 0 && index < autoClickCosts.Count && index < autoClickPowers.Count)
        {
            double cost = autoClickCosts[index];
            double power = autoClickPowers[index];

            // ���������, ������� �� �������� ��� ������� ���������
            if (resourceManager.SpendResources(cost))
            {
                autoClickPower += power;
                UpdateAutoClickPowerText();
                SaveAutoClickPower(); // ���������� �������� ��������� ����� �������

                // �������� ���������, ���� ��� ��� �� ���� ��������
                if (!autoClickEnabled)
                {
                    autoClickEnabled = true;
                    InvokeRepeating(nameof(PerformAutoClick), autoClickInterval, autoClickInterval);
                }
            }
        }
    }

    // ����� ��� ���������� ���������
    private void PerformAutoClick()
    {
        resourceManager.AddResources(autoClickPower);
    }

    // ����� ��� ���������� ������ ���� ���������
    private void UpdateAutoClickPowerText()
    {
        if (autoClickPowerText != null)
        {
            autoClickPowerText.text = $" {autoClickPower:F2}";
        }
    }

    // ����� ��� ��������� ������� ���� ��������� (���� �����)
    public double GetAutoClickPower()
    {
        return autoClickPower;
    }

    // ����� ��� ���������� �������� ���������
    public void IncreaseAutoClickPower(double amount)
    {
        autoClickPower *= amount;
        UpdateAutoClickPowerText();
        SaveAutoClickPower(); // ���������� �������� ��������� ����� ����������
    }

    // ����� ��� ���������� ������� �������� ���������
    private void SaveAutoClickPower()
    {
        YandexGame.savesData.autoClickPower = autoClickPower;
        YandexGame.SaveProgress();
    }

    // ����� ��� �������� ����������� �������� ���������
    private void LoadAutoClickPower()
    {
        if (YandexGame.savesData.autoClickPower != 0)
        {
            autoClickPower = YandexGame.savesData.autoClickPower;
        }
    }
}