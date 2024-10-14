using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI clickText;
    private int clickCount = 0;

    public void OnClick()
    {
        clickCount++;
        clickText.text = "Клики: " + clickCount.ToString();
    }
   
}
