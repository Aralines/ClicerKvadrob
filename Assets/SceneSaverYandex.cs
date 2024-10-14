using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class SceneSaverYandex : MonoBehaviour
{
    void Start()
    {
        // ��������� ����������� ����� ��� ������� ����
        if (YandexGame.SDKEnabled && YandexGame.savesData != null)
        {
            LoadScene();
        }
    }

    // ����� ��� ������������ ����� � ���������� � �����
    public void ChangeScene(string sceneName)
    {
        if (YandexGame.SDKEnabled)
        {
            SaveScene(sceneName);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Yandex SDK �� �������!");
        }
    }

    // ���������� ������� �����
    private void SaveScene(string sceneName)
    {
        if (YandexGame.savesData != null)
        {
            YandexGame.savesData.savedSceneName = sceneName;
            YandexGame.SaveProgress();
        }
    }

    // �������� ����������� �����
    private void LoadScene()
    {
        if (!string.IsNullOrEmpty(YandexGame.savesData.savedSceneName) &&
            SceneManager.GetActiveScene().name != YandexGame.savesData.savedSceneName)
        {
            SceneManager.LoadScene(YandexGame.savesData.savedSceneName);
        }
    }
}
