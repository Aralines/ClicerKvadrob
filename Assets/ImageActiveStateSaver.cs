using UnityEngine;
using YG;

public class ImageActiveStateSaver : MonoBehaviour
{
    public GameObject image1; // ������ ��������
    public GameObject image2; // ������ ��������

    void Start()
    {
        LoadActiveStates(); // �������� ������������ ��������� ���������� ��������
    }

    // ����� ��� ���������� ��������� ���������� ��������
    public void SaveActiveStates()
    {
        YandexGame.savesData.image1Active = image1.activeSelf;
        YandexGame.savesData.image2Active = image2.activeSelf;
        YandexGame.SaveProgress();
    }

    // ����� ��� �������� ��������� ���������� ��������
    private void LoadActiveStates()
    {
        if (YandexGame.savesData != null)
        {
            image1.SetActive(YandexGame.savesData.image1Active);
            image2.SetActive(YandexGame.savesData.image2Active);
        }
    }

    // ����� ��� ��������� ��������� ���������� �������� � ���������� �� ���������
    public void SetImageActive(GameObject image, bool isActive)
    {
        image.SetActive(isActive);
        SaveActiveStates();
    }
}
