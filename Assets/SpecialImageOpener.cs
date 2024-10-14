using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using UnityEngine.UI;

public class SpecialImageOpener : MonoBehaviour
{
    public Button targetButton;          // ������, �� ������� ����� ����������� ������
    public Sprite requiredSprite;        // ������, ������� ������ ���� �� ������
    public Image imageToActivate1;       // ������ �����������, ������� ������ ��������
    public Image imageToActivate2;       // ������ �����������, ������� ������ ��������
    public Image imageToActivate3;       // ������ �����������, ������� ������ ��������
    public Image imageToActivate4;       // ��������� �����������, ������� ������ ����������
    public GameObject gameObject1;

    void Start()
    {
        // ���������, ��� ��� ������ ��������� ����� ���������
        if (targetButton == null || requiredSprite == null || imageToActivate1 == null || imageToActivate2 == null || imageToActivate3 == null || imageToActivate4 == null)
        {
            Debug.LogError("�� ��� ������ ��������� � ����������.");
            return;
        }

        // �������� ��������� �����������
        LoadImageStates();

        // ����������� ����� � ������, ����� ��������� ������ ��� ������ �������
        targetButton.onClick.AddListener(CheckButtonSprite);
    }

    // ����� ��� �������� ������� ������
    private void CheckButtonSprite()
    {
        if (targetButton.image.sprite == requiredSprite)
        {
            ActivateImages();
        }
    }

    // ����� ��� ��������� �����������
    private void ActivateImages()
    {
        imageToActivate1.gameObject.SetActive(true);
        imageToActivate2.gameObject.SetActive(true);
        imageToActivate3.gameObject.SetActive(true);
        imageToActivate4.gameObject.SetActive(false);
        Debug.Log("����������� ������������ � ��������������.");
        Destroy(gameObject1);
        SaveImageStates();
    }

    // ����� ��� ���������� ��������� �����������
    private void SaveImageStates()
    {
        YandexGame.savesData.imageToActivate2State = imageToActivate2.gameObject.activeSelf;
        YandexGame.savesData.imageToActivate4State = imageToActivate4.gameObject.activeSelf;
        YandexGame.SaveProgress();
    }

    // ����� ��� �������� ��������� �����������
    private void LoadImageStates()
    {
        if (YandexGame.savesData != null)
        {
            imageToActivate2.gameObject.SetActive(YandexGame.savesData.imageToActivate2State);
            imageToActivate4.gameObject.SetActive(YandexGame.savesData.imageToActivate4State);
        }
    }
}
