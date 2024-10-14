using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ButtonColorManager : MonoBehaviour
{
    public UpgradeManager upgradeManager; // ������ �� UpgradeManager ��� �������� ��������
    public List<Button> upgradeButtons; // ������ ������ ��� ���������
    public Color defaultColor; // �������� ���� ������
    public Color clickedColor; // ���� ������ ����� �������

    void Start()
    {
        // ������������� ������� ������ �� ����������� ������
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            int index = i; // ��������� ���������� ��� �������������� ������� � ����������
            upgradeButtons[i].onClick.AddListener(() => OnButtonClick(index));

            // �������� ��������� ������ �� ����������
            if (YandexGame.savesData.buttonClickedStatus != null && i < YandexGame.savesData.buttonClickedStatus.Length)
            {
                bool isClicked = YandexGame.savesData.buttonClickedStatus[i];
                upgradeButtons[i].GetComponent<Image>().color = isClicked ? clickedColor : defaultColor;
            }
            else
            {
                upgradeButtons[i].GetComponent<Image>().color = defaultColor;
            }
        }
    }

    void OnButtonClick(int index)
    {
        double cost = upgradeManager.resourceCosts[index];
        if (HasSufficientResources(cost))
        {
            upgradeButtons[index].GetComponent<Image>().color = clickedColor; // ��������� ����� ������ ����� �������, ���� ������� ��������

            // ���������� ������� ������
            if (YandexGame.savesData.buttonClickedStatus == null || YandexGame.savesData.buttonClickedStatus.Length < upgradeButtons.Count)
            {
                YandexGame.savesData.buttonClickedStatus = new bool[upgradeButtons.Count];
            }
            YandexGame.savesData.buttonClickedStatus[index] = true;

            // ���������� ����
            YandexGame.SaveProgress();
        }
    }

    bool HasSufficientResources(double cost)
    {
        return upgradeManager.resourceManager.GetResources() >= cost;
    }
}