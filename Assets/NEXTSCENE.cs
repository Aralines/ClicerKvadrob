using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NEXTSCENE : MonoBehaviour
{
    // Название или индекс сцены, на которую вы хотите переключиться
    public string sceneName;
    public string sceneName1;
    // Метод, который будет привязан к событию нажатия кнопки
    public void SwitchScene()
    {
        // Загружаем сцену по имени
        SceneManager.LoadScene(sceneName);
    }
    public void SwitchScene2()
    {
        // Загружаем сцену по имени
        SceneManager.LoadScene(sceneName1);
    }
}
