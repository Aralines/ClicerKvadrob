using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColor : MonoBehaviour
{
    public List<Image> primaryImages;     // ������ �������� �����������, ������� �������������
    public List<Image> secondaryImages;   // ������ ��������� �����������, ������� ������ ����

    void Start()
    {
        // ���������, ��� ������ �� ����������� �����������
        if (primaryImages == null || secondaryImages == null)
        {
            Debug.LogError("�� ��� ������ �� ����������� �����������.");
            return;
        }

        // ��������, ��� ���������� �������� � ��������� ����������� ���������
        if (primaryImages.Count != secondaryImages.Count)
        {
            Debug.LogError("���������� �������� � ��������� ����������� ������ ���������.");
            return;
        }
    }

    // �����, ������� ��������� � �������� �����
    public void UpdateImageColors()
    {
        // ��������� ������ ������� �� ���������
        for (int i = 0; i < primaryImages.Count; i++)
        {
            // ���� �������� ����������� ����� ����� ����, ������������� ����� �� ���� ��� ����������
            if (primaryImages[i] != null && secondaryImages[i] != null)
            {
                if (primaryImages[i].color == Color.white)
                {
                    secondaryImages[i].color = Color.white;
                    Debug.Log($"����������� � �������� {i} �������� �� ����� ����.");
                }
            }
        }
    }
}
