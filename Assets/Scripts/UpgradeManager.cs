using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public ResourceManager resourceManager;            // ������ �� ResourceManager ��� ������ � ���������
    public ClickResourceAdder clickResourceAdder;      // ������ �� ClickResourceAdder ��� ��������� gainAmount
    public List<double> resourceCosts;                 // ������ ��������� ��������� � ��������
    public List<double> clickPowerIncreases;           // ������ �������� �������� �� ����
    public List<Button> upgradeButtons;                // ������ ������ ��� ������ ���������

    void Start()
    {
        // ��������� ������� ������ �� ResourceManager, ClickResourceAdder � ������
        if (resourceManager == null || clickResourceAdder == null)
        {
            Debug.LogError("�� ������� ����� ��������� ResourceManager ��� ClickResourceAdder.");
            return;
        }

        // ���������, ��� ���������� ������ ������������� ���������� ���������
        if (resourceCosts.Count != clickPowerIncreases.Count || resourceCosts.Count != upgradeButtons.Count)
        {
            Debug.LogError("���������� ��������� � ������� resourceCosts, clickPowerIncreases � upgradeButtons ������ ���������.");
            return;
        }

        // ����������� ����� � ������� OnClick ������ ������
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            int index = i; // ��������� ����� ���������� i, ����� �������� ������� � ����������
            upgradeButtons[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    // �����, ������� ���������� ��� ������� �� ������
    private void OnButtonClick(int index)
    {
        // ���������, ��� ������ � �������� ������
        if (index < 0 || index >= resourceCosts.Count)
        {
            Debug.LogError("������ ��� ��������� ������.");
            return;
        }

        // �������� ��������� ��������� � ������� ����� �� �������
        double cost = resourceCosts[index];
        double increase = clickPowerIncreases[index];

        // ���������, ������� �� �������� ��� ������� ���������
        if (resourceManager.SpendResources(cost))
        {
            // ����������� gainAmount � ClickResourceAdder
            clickResourceAdder.IncreaseGainAmount(increase);
            Debug.Log($"������� ��������� �� {cost} ��������! ��������� {increase} � ���� ������.");
        }
        else
        {
            Debug.Log("������������ �������� ��� ������� ���������.");
        }
    }
}
