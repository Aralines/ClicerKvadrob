using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColor : MonoBehaviour
{
    public List<Image> primaryImages;     // Список основных изображений, которые отслеживаются
    public List<Image> secondaryImages;   // Список вторичных изображений, которые меняют цвет

    void Start()
    {
        // Проверяем, что ссылки на изображения установлены
        if (primaryImages == null || secondaryImages == null)
        {
            Debug.LogError("Не все ссылки на изображения установлены.");
            return;
        }

        // Убедимся, что количество основных и вторичных изображений совпадает
        if (primaryImages.Count != secondaryImages.Count)
        {
            Debug.LogError("Количество основных и вторичных изображений должно совпадать.");
            return;
        }
    }

    // Метод, который проверяет и изменяет цвета
    public void UpdateImageColors()
    {
        // Проверяем каждый элемент из коллекции
        for (int i = 0; i < primaryImages.Count; i++)
        {
            // Если основное изображение имеет белый цвет, устанавливаем такой же цвет для вторичного
            if (primaryImages[i] != null && secondaryImages[i] != null)
            {
                if (primaryImages[i].color == Color.white)
                {
                    secondaryImages[i].color = Color.white;
                    Debug.Log($"Изображение с индексом {i} изменено на белый цвет.");
                }
            }
        }
    }
}
