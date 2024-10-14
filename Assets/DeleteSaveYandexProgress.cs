using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class DeleteSaveYandexProgress : MonoBehaviour
{
    // Метод для сброса всех облачных сохранений
    public void ResetAllSaves()
    {
        YandexGame.savesData = new SavesYG(); // Создаем новый объект сохранений, чтобы сбросить все данные
        YandexGame.SaveProgress(); // Сохраняем пустые данные в облако, перезаписывая существующие
        Debug.Log("Все облачные сохранения сброшены.");
    }
}
