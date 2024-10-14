using UnityEngine;
using YG;

public class ImageActiveStateSaver : MonoBehaviour
{
    public GameObject image1; // ѕерва€ картинка
    public GameObject image2; // ¬тора€ картинка

    void Start()
    {
        LoadActiveStates(); // «агрузка сохраненного состо€ни€ активности картинок
    }

    // ћетод дл€ сохранени€ состо€ни€ активности картинок
    public void SaveActiveStates()
    {
        YandexGame.savesData.image1Active = image1.activeSelf;
        YandexGame.savesData.image2Active = image2.activeSelf;
        YandexGame.SaveProgress();
    }

    // ћетод дл€ загрузки состо€ни€ активности картинок
    private void LoadActiveStates()
    {
        if (YandexGame.savesData != null)
        {
            image1.SetActive(YandexGame.savesData.image1Active);
            image2.SetActive(YandexGame.savesData.image2Active);
        }
    }

    // ћетод дл€ изменени€ состо€ни€ активности картинок и сохранени€ их состо€ни€
    public void SetImageActive(GameObject image, bool isActive)
    {
        image.SetActive(isActive);
        SaveActiveStates();
    }
}
