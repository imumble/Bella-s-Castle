using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySelect : MonoBehaviour
{
    public BattleSystem battleSystem;
    public GameObject enemySelectPanel;

    public GameObject enemySlot1Panel;
    public GameObject enemySlot2Panel;
    public GameObject enemySlot3Panel;

    public Text enemyNameSlot1;
    public Text enemyNameSlot2;
    public Text enemyNameSlot3;

    public Text enemyHPSlot1;
    public Text enemyHPSlot2;
    public Text enemyHPSlot3;

    public void ShowEnemySelectPanel()
    {
        //Decide how many enemys to show in the panel
        firstPanel();

        secondPanel();

        thirdPanel();

        enemySelectPanel.SetActive(true);
    }

    private void firstPanel()
    {
        if (battleSystem.enemyUnit != null)
        {
            enemyNameSlot1.text = battleSystem.enemyUnit.unitName;
            enemyHPSlot1.text = battleSystem.enemyUnit.currentHP + "/" + battleSystem.enemyUnit.maxHP;
        }
        else
        {
            enemySlot1Panel.SetActive(false);
        }
    }

    private void secondPanel()
    {
        //Is there a second enemy
        if (battleSystem.enemy2Unit != null)
        {
            //are we the same unit as the first slot?
            if (battleSystem.enemyUnit != null && battleSystem.enemyUnit.unitName == battleSystem.enemy2Unit.unitName)
            {
                enemyNameSlot2.text = battleSystem.enemy2Unit.unitName + "2";
            }
            else
            {
                enemyNameSlot2.text = battleSystem.enemy2Unit.unitName;
            }
            enemyHPSlot2.text = battleSystem.enemy2Unit.currentHP + "/" + battleSystem.enemy2Unit.maxHP;
        }
        else
        {
            enemySlot2Panel.SetActive(false);
        }
    }

    private void thirdPanel()
    {
        //is there third enemy
        if (battleSystem.enemy3Unit != null)
        {

            if (battleSystem.enemyUnit != null && battleSystem.enemyUnit.unitName == battleSystem.enemy3Unit.unitName)
            {
                enemyNameSlot3.text = battleSystem.enemy3Unit.unitName + "2";
            }

            if (battleSystem.enemyUnit != null && battleSystem.enemy2Unit != null)
            {
                if (battleSystem.enemy2Unit.unitName == battleSystem.enemy3Unit.unitName)
                {
                    enemyNameSlot3.text = battleSystem.enemy3Unit.unitName + "3";
                }
                else
                {
                    enemyNameSlot3.text = battleSystem.enemy3Unit.unitName;
                }
            }
            enemyHPSlot3.text = battleSystem.enemy3Unit.currentHP + "/" + battleSystem.enemy3Unit.maxHP;
        }
        else
        {
            enemySlot3Panel.SetActive(false);
        }
    }

    public void AttackSlot1()
    {
        AttackBadGuy(1);
    }
    public void AttackSlot2()
    {
        AttackBadGuy(2);
    }
    public void AttackSlot3()
    {
        AttackBadGuy(3);
    }

    public void AttackBadGuy(int slot)
    {
        if (battleSystem.regularAttack)
        {
            StartCoroutine(battleSystem.PlayerAttack(slot));
        }
        else
        {
            StartCoroutine(battleSystem.PlayerTimedAttack(slot));
        }
        
        enemySelectPanel.SetActive(false);
    }
}
