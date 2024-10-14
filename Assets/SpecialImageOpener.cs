using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using UnityEngine.UI;

public class SpecialImageOpener : MonoBehaviour
{
    public Button targetButton;          // Кнопка, на которой будет проверяться спрайт
    public Sprite requiredSprite;        // Спрайт, который должен быть на кнопке
    public Image imageToActivate1;       // Первое изображение, которое станет активным
    public Image imageToActivate2;       // Второе изображение, которое станет активным
    public Image imageToActivate3;       // Третье изображение, которое станет активным
    public Image imageToActivate4;       // Четвертое изображение, которое станет неактивным
    public GameObject gameObject1;

    void Start()
    {
        // Проверяем, что все ссылки назначены через инспектор
        if (targetButton == null || requiredSprite == null || imageToActivate1 == null || imageToActivate2 == null || imageToActivate3 == null || imageToActivate4 == null)
        {
            Debug.LogError("Не все ссылки назначены в инспекторе.");
            return;
        }

        // Загрузка состояния изображений
        LoadImageStates();

        // Привязываем метод к кнопке, чтобы проверять спрайт при каждом нажатии
        targetButton.onClick.AddListener(CheckButtonSprite);
    }

    // Метод для проверки спрайта кнопки
    private void CheckButtonSprite()
    {
        if (targetButton.image.sprite == requiredSprite)
        {
            ActivateImages();
        }
    }

    // Метод для активации изображений
    private void ActivateImages()
    {
        imageToActivate1.gameObject.SetActive(true);
        imageToActivate2.gameObject.SetActive(true);
        imageToActivate3.gameObject.SetActive(true);
        imageToActivate4.gameObject.SetActive(false);
        Debug.Log("Изображения активированы и деактивированы.");
        Destroy(gameObject1);
        SaveImageStates();
    }

    // Метод для сохранения состояния изображений
    private void SaveImageStates()
    {
        YandexGame.savesData.imageToActivate2State = imageToActivate2.gameObject.activeSelf;
        YandexGame.savesData.imageToActivate4State = imageToActivate4.gameObject.activeSelf;
        YandexGame.SaveProgress();
    }

    // Метод для загрузки состояния изображений
    private void LoadImageStates()
    {
        if (YandexGame.savesData != null)
        {
            imageToActivate2.gameObject.SetActive(YandexGame.savesData.imageToActivate2State);
            imageToActivate4.gameObject.SetActive(YandexGame.savesData.imageToActivate4State);
        }
    }
}
