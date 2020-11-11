using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverworldStuff : MonoBehaviour
{
    public Text PlayerHealthText;
    // Update is called once per frame
    void Update()
    {
        PlayerHealthText.text = "Player Health:"+MainPlayer.playerCurrentHealth + "/" + MainPlayer.playerMaxHealth;
    }
}
