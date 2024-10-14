using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class DeleteSaveYandexProgress : MonoBehaviour
{
    // ����� ��� ������ ���� �������� ����������
    public void ResetAllSaves()
    {
        YandexGame.savesData = new SavesYG(); // ������� ����� ������ ����������, ����� �������� ��� ������
        YandexGame.SaveProgress(); // ��������� ������ ������ � ������, ������������� ������������
        Debug.Log("��� �������� ���������� ��������.");
    }
}
