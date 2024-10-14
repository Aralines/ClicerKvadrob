using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisappearOnInput : MonoBehaviour
{
    void Update()
    {
        // �������� �� ����� ������� �������, ������ ���� ��� ���� �� ������
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Destroy(gameObject); // ���������� ������
        }

    }
}

