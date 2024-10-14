using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class SceneSaverYandex : MonoBehaviour
{
    void Start()
    {
        // Загружаем сохраненную сцену при запуске игры
        if (YandexGame.SDKEnabled && YandexGame.savesData != null)
        {
            LoadScene();
        }
    }

    // Метод для переключения сцены и сохранения её имени
    public void ChangeScene(string sceneName)
    {
        if (YandexGame.SDKEnabled)
        {
            SaveScene(sceneName);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Yandex SDK не включен!");
        }
    }

    // Сохранение текущей сцены
    private void SaveScene(string sceneName)
    {
        if (YandexGame.savesData != null)
        {
            YandexGame.savesData.savedSceneName = sceneName;
            YandexGame.SaveProgress();
        }
    }

    // Загрузка сохраненной сцены
    private void LoadScene()
    {
        if (!string.IsNullOrEmpty(YandexGame.savesData.savedSceneName) &&
            SceneManager.GetActiveScene().name != YandexGame.savesData.savedSceneName)
        {
            SceneManager.LoadScene(YandexGame.savesData.savedSceneName);
        }
    }
}
