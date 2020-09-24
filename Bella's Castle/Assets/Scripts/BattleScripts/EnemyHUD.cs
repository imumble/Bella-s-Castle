using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHUD : MonoBehaviour
{
    public Text CurrentEnemyHealth;

    public void SetEnemyHUD(EnemyUnit enemy)
    {
        CurrentEnemyHealth.text = enemy.currentHP + "/" + enemy.maxHP;
    }
}
