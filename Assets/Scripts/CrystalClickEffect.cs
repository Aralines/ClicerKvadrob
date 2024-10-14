using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalClickEffect : MonoBehaviour
{
    public GameObject crystalPrefab;  // Префаб кристалла
    public Transform canvas;  // Canvas для размещения кристаллов
    public Button clickButton;  // Ссылка на кнопку

    void Start()
    {
        // Привязываем функцию к событию нажатия на кнопку
        clickButton.onClick.AddListener(SpawnCrystalAtMousePosition);
    }

    void SpawnCrystalAtMousePosition()
    {
        // Получаем позицию курсора
        Vector3 mousePosition = Input.mousePosition;

        // Создаём кристалл на позиции курсора
        GameObject newCrystal = Instantiate(crystalPrefab, canvas);
        newCrystal.transform.position = mousePosition;

        // Отключаем возможность клика по кристаллу
        DisableRaycastTarget(newCrystal);

        // Запускаем анимацию кристалла
        StartCoroutine(MoveAndFadeOut(newCrystal));
    }

    void DisableRaycastTarget(GameObject crystal)
    {
        // Отключаем Raycast Target на всех UI-элементах кристалла
        Image crystalImage = crystal.GetComponent<Image>();
        if (crystalImage != null)
        {
            crystalImage.raycastTarget = false;  // Отключаем клики
        }
    }

    IEnumerator MoveAndFadeOut(GameObject crystal)
    {
        float duration = 1.5f;  // Длительность анимации
        float elapsed = 0f;
        CanvasGroup canvasGroup = crystal.AddComponent<CanvasGroup>();  // Добавляем CanvasGroup для прозрачности

        Vector3 startPos = crystal.transform.position;
        Vector3 endPos = startPos + new Vector3(0, 50, 0);  // Поднимаем кристалл на 50 пикселей вверх

        while (elapsed < duration)
        {
            // Плавное перемещение кристалла вверх
            crystal.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);

            // Плавное уменьшение прозрачности
            canvasGroup.alpha = 1 - (elapsed / duration);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Уничтожаем кристалл после завершения анимации
        Destroy(crystal);
    }
}
