using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ButtonSpriteSwapper : MonoBehaviour
{
    public Spriteman spriteManager; // ������ �� SpriteManager ��� ������� � ��������
    public Button mainButton;            // �������� ������, �� ������� ����� �������� ������
    public List<Button> buttons;         // ������ ������, ������ ������ ����� ���� ������

    void Start()
    {
        if (spriteManager == null || mainButton == null || buttons == null)
        {
            Debug.LogError("�� ��� ������ ��������� � ����������.");
            return;
        }

        // ��������� ������ ������ �������� ��� �������
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i; // �������� ������ � ��������� ����������, ����� �������� ������� � ����������
            buttons[i].onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    private void OnButtonClicked(int buttonIndex)
    {
        if (buttonIndex >= 0 && buttonIndex < spriteManager.sprites.Count)
        {
            // ������������� ������ �� �������� ������ �� SpriteManager
            mainButton.image.sprite = spriteManager.sprites[buttonIndex];
            SaveMainButtonSprite(buttonIndex); // ��������� ��������� �������� �������
        }
        else
        {
            Debug.LogWarning("������ ������ �� ��������� ���������.");
        }
    }

    // ����� ��� ���������� �������� ������� ������� ������
    private void SaveMainButtonSprite(int spriteIndex)
    {
        YandexGame.savesData.mainButtonSpriteIndex = spriteIndex;
        YandexGame.SaveProgress();
    }

    // ����� ��� �������� ������� ������� ������
    private void LoadMainButtonSprite()
    {
        if (YandexGame.savesData.mainButtonSpriteIndex >= 0 && YandexGame.savesData.mainButtonSpriteIndex < spriteManager.sprites.Count)
        {
            mainButton.image.sprite = spriteManager.sprites[YandexGame.savesData.mainButtonSpriteIndex];
        }
    }
}