using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoClickButtonColorManager : MonoBehaviour
{
    public AutoClickManager autoClickManager; // ������ �� AutoClickManager ��� �������� ��������
    public List<Button> upgradeButtons; // ������ ������ ��� ���������
    public Color defaultColor; // �������� ���� ������
    public Color clickedColor; // ���� ������ ����� �������

    void Start()
    {
        // �������� ������� � �������
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            int index = i; // ��������� ���������� ��� �������������� ������� � ����������
            upgradeButtons[i].onClick.AddListener(() => OnButtonClick(index));
            upgradeButtons[i].GetComponent<Image>().color = defaultColor; // ��������� ���������� ����� ������
        }
    }

    void OnButtonClick(int index)
    {
        if (index >= 0 && index < autoClickManager.autoClickCosts.Count)
        {
            double cost = autoClickManager.autoClickCosts[index];
            if (HasSufficientResources(cost))
            {
                autoClickManager.BuyAutoClick(index); // ������� ���������
                upgradeButtons[index].GetComponent<Image>().color = clickedColor; // ��������� ����� ������ ����� �������, ���� ������� ��������
            }
        }
    }

    bool HasSufficientResources(double cost)
    {
        return autoClickManager.resourceManager.GetResources() >= cost;
    }
}



