using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySelect : MonoBehaviour
{
    public BattleSystem battleSystem;
    public GameObject enemySelectPanel;

    public void ShowEnemySelectPanel()
    {
        enemySelectPanel.SetActive(true);
    }

    public void CallAttack()
    {
        StartCoroutine(battleSystem.PlayerAttack());
        enemySelectPanel.SetActive(false);
    }
}
