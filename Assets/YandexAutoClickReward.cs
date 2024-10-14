using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class YandexAutoClickReward : MonoBehaviour
{
    public AutoClickManager autoClickManager; 
    public Button rewardButton; 

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
        if (rewardButton != null)
        {
            rewardButton.onClick.AddListener(ShowRewardAd);
        }
    }

    private void OnDisable() 
    {
        YandexGame.RewardVideoEvent -= Rewarded;
        if (rewardButton != null)
        {
            rewardButton.onClick.RemoveListener(ShowRewardAd);
        }
    }

    // Метод для обработки награды после просмотра рекламы
    private void Rewarded(int id)
    {
        if (id == 1) 
        {
            autoClickManager.IncreaseAutoClickPower(2);
            
        }
    }
    public int gold;

    private void Start()
    {
        LoadGame();
    }
    public void LoadGame()
    {
        gold = YandexGame.savesData.goldDataSave;
        Debug.Log(gold);
    }
    public void SaveGame()
    {
        YandexGame.savesData.goldDataSave = gold;
        YandexGame.SaveProgress();
        Debug.Log(gold);

    }
    public void PPPLUSgold()
    {
        gold += 100;
        Debug.Log("Прибавляем 100");
        SaveGame();
    }


    // Метод для показа рекламы
    private void ShowRewardAd()
    {
        YandexGame.RewVideoShow(1); // Показываем рекламное видео с ID 1
    }
}
