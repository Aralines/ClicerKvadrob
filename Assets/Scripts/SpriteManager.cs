using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SpriteManager : MonoBehaviour
{
    public Button mainButton;                 // �������� ������, � ������� ����� �������� ������
    public List<Sprite> sprites;              // ��������� ��������
    public List<Button> buttons;              // ������ ������, ������ ����� ������ ������
    public List<int> requiredResources;       // ������, �������� ���������� �������� ��� ������ ������
    public List<Image> previewImages;         // ������ �����������, ������� ����� ������������ � ����� ����
    public ResourceManager resourceManager;   // ������ �� ResourceManager ��� �������� ��������
    public List<Button> extraButtons;         // �������������� ������, ������ �� ������� ����� ��������� ������ ������ �� ������� ������

    private List<bool> hasSpentResources;     // ������ ������ ��� ������������, ��������� �� ������� ��� ������ ������

    void Start()
    {
        // ���������, ��� � ��� ���� ������ �� �������, �������� ������, �������������� ������, ����������� � ���������� ��������
        if (mainButton == null || sprites == null || buttons == null || resourceManager == null || requiredResources == null || previewImages == null || extraButtons == null)
        {
            Debug.LogError("�� ��� ������ �����������.");
            return;
        }

        // ��������, ��� ���������� ��������, ������, �������� � ����������� �������������
        if (sprites.Count != buttons.Count || requiredResources.Count != buttons.Count || previewImages.Count != buttons.Count)
        {
            Debug.LogError("���������� ��������, ������, ��������� �������� � ����������� ������ ���������.");
            return;
        }

        // �������������� ������ ������, ����� �����������, ��������� �� ������� ��� ������ ������
        hasSpentResources = new List<bool>(new bool[buttons.Count]);

        // �������� ��������� ������ � �������� ������� �� ����������
        LoadButtonStates();
        LoadMainButtonSprite();

        // ��������� ������ ������ �������� ��� �������
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i;  // �������� ������ � ��������� ����������, ����� �������� ������� � ����������
            buttons[i].onClick.AddListener(() => ChangeMainButtonSprite(index));

            // ������������� ��������� previewImages � ����������� �� ����������� ������
            if (previewImages[i] != null)
            {
                previewImages[i].color = hasSpentResources[i] ? Color.white : Color.black;
            }
        }

        // ��������� ������ �������������� ������ �������� ��� �������
        foreach (Button extraButton in extraButtons)
        {
            extraButton.onClick.AddListener(() => ChangeMainButtonSpriteToExtra(extraButton));
        }
    }

    // ����� ��� ��������� ������� �������� ������
    private void ChangeMainButtonSprite(int spriteIndex)
    {
        // ���������, ���������� �� �������� ��� ����� ������� � ������ �� ������ ���� ���
        if (!hasSpentResources[spriteIndex])
        {
            int requiredResourceAmount = requiredResources[spriteIndex];
            if (resourceManager.GetResources() >= requiredResourceAmount)
            {
                if (resourceManager.SpendResources(requiredResourceAmount))
                {
                    hasSpentResources[spriteIndex] = true; // ������������� ����, ����� �� ������� ������� �������� ��� ���� ������
                    ApplySpriteChange(spriteIndex);
                    ChangePreviewImageToWhite(spriteIndex); // �������� ���� ����������� �� ����� ������ ���� ������� ������� ���������
                    SaveButtonStates(); // ���������� ��������� ������
                    SaveMainButtonSprite(spriteIndex); // ���������� ��������� �������� �������
                    YandexGame.savesData.extraButtonSpriteName = ""; // ���������� ����������� ������ �� extraButtons
                    YandexGame.SaveProgress();
                    Debug.Log($"������� ���������, ������ �������� ������ ������� ��: {sprites[spriteIndex].name}");
                }
                else
                {
                    Debug.LogWarning("�� ������� ��������� �������.");
                }
            }
            else
            {
                Debug.LogWarning($"������������ �������� ��� ��������� �������. ���������: {requiredResourceAmount}");
            }
        }
        else
        {
            // ���� ������� ��� ���� ���������, ������ ��������� ��������� ������� ��� ����� ��������
            ApplySpriteChange(spriteIndex);
            SaveMainButtonSprite(spriteIndex); // ���������� ��������� �������� �������
            YandexGame.savesData.extraButtonSpriteName = ""; // ���������� ����������� ������ �� extraButtons
            YandexGame.SaveProgress();
        }
    }

    // ����� ��� ��������� ������� �������� ������ �� ������, ������� ��������� �� �������������� ������
    private void ChangeMainButtonSpriteToExtra(Button extraButton)
    {
        Sprite newSprite = extraButton.image.sprite;

        if (newSprite != null)
        {
            // ������������� ����� ������ �� �������� ������
            mainButton.image.sprite = newSprite;

            // ��������� ��� �������, ����� ����� ����� ���� ����� ���
            YandexGame.savesData.extraButtonSpriteName = newSprite.name;
            YandexGame.savesData.mainButtonSpriteIndex = -1; // ���������� ������ ��������� �������
            YandexGame.SaveProgress();

            Debug.Log("������ �������� ������ ������� �� ������ �������������� ������: " + newSprite.name);
        }
    }

    // ����� ��� ���������� ��������� �������
    private void ApplySpriteChange(int spriteIndex)
    {
        if (spriteIndex >= 0 && spriteIndex < sprites.Count)
        {
            mainButton.image.sprite = sprites[spriteIndex];
        }
        else
        {
            Debug.LogWarning("������ ������� �� ��������� ���������.");
        }
    }

    // ����� ��� ��������� ����� ����������� previewImage �� �����
    private void ChangePreviewImageToWhite(int spriteIndex)
    {
        if (previewImages[spriteIndex] != null)
        {
            previewImages[spriteIndex].color = Color.white;
            Debug.Log("����������� �������� � ����� ���� ��� �������: " + spriteIndex);
        }
    }

    // ����� ��� ���������� ��������� ������
    private void SaveButtonStates()
    {
        YandexGame.savesData.buttonClickedStatus = hasSpentResources.ToArray();
        YandexGame.SaveProgress();
    }

    // ����� ��� �������� ��������� ������
    private void LoadButtonStates()
    {
        if (YandexGame.savesData.buttonClickedStatus != null && YandexGame.savesData.buttonClickedStatus.Length == buttons.Count)
        {
            hasSpentResources = new List<bool>(YandexGame.savesData.buttonClickedStatus);
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
        if (YandexGame.savesData.mainButtonSpriteIndex >= 0 && YandexGame.savesData.mainButtonSpriteIndex < sprites.Count)
        {
            mainButton.image.sprite = sprites[YandexGame.savesData.mainButtonSpriteIndex];
        }
        else if (!string.IsNullOrEmpty(YandexGame.savesData.extraButtonSpriteName))
        {
            // ���� ������ �� ����� ����� extraButtons
            foreach (Button extraButton in extraButtons)
            {
                if (extraButton.image.sprite != null && extraButton.image.sprite.name == YandexGame.savesData.extraButtonSpriteName)
                {
                    mainButton.image.sprite = extraButton.image.sprite;
                    break;
                }
            }
        }
    }
}