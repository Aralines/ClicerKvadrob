using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NEXTSCENE : MonoBehaviour
{
    // �������� ��� ������ �����, �� ������� �� ������ �������������
    public string sceneName;
    public string sceneName1;
    // �����, ������� ����� �������� � ������� ������� ������
    public void SwitchScene()
    {
        // ��������� ����� �� �����
        SceneManager.LoadScene(sceneName);
    }
    public void SwitchScene2()
    {
        // ��������� ����� �� �����
        SceneManager.LoadScene(sceneName1);
    }
}
