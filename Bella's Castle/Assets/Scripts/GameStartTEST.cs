using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStartTEST : MonoBehaviour
{
    public InputField inputField;

    public void StartGame()
    {
        MainPlayer.playerName = inputField.text;
        MainPlayer.playerMaxHealth = 25;
        MainPlayer.playerCurrentHealth = 25;
        MainPlayer.playerATK = 3;
        MainPlayer.playerDEF = 5;
        MainPlayer.currentXP = 0;
        MainPlayer.playerLevel = 1;


        SceneManager.LoadScene(2); // 2=overworldcombat
    }
}
