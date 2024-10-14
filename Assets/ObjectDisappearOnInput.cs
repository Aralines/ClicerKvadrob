using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisappearOnInput : MonoBehaviour
{
    void Update()
    {
        // Проверка на любое нажатие клавиши, кнопки мыши или тапа на экране
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Destroy(gameObject); // Уничтожаем объект
        }

    }
}

