using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Text Player1Health;
    public Text Player1MP;
    public Text Player2Health;
    public Text Player2MP;
    
    public Slider comboSlider;

    public void SetHUD(Unit player1, Unit player2)
    {
        Player1Health.text = player1.currentHP  + "/" + player1.maxHP;
        Player1MP.text = player1.currentMP + "/" + player1.maxMP;

        Player2Health.text = player2.currentHP + "/" + player2.maxHP;
        Player2MP.text = player2.currentMP + "/" + player2.maxMP;
    }
    

}
