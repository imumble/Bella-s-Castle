using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, PLAYER2TURN, ENEMYSTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject warriorPrefab;
    public GameObject magePrefab;
    public GameObject enemyPrefab;

    public Transform warriorBattleStation;
    public Transform mageBattleStation;
    public Transform enemyBattleStation;
    public Transform enemy2BattleStation;
    public Transform enemy3BattleStation;

    public PlayerHUD PlayerHUD;
    public EnemyHUD EnemyHUD;

    public bool secondEnemy;
    public bool thirdEnemy;

    Unit warriorUnit;
    Unit mageUnit;
    EnemyUnit enemyUnit;
    EnemyUnit enemy2Unit;
    EnemyUnit enemy3Unit;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;

        //Overworld attack takes place

        StartCoroutine(SetupBattle());
    }


    IEnumerator SetupBattle()
    {
        //for testing
        secondEnemy = true;
        thirdEnemy = true;

        Debug.Log("SetupBattle");
        GameObject warriorGO = Instantiate(warriorPrefab, warriorBattleStation);
        warriorUnit = warriorGO.GetComponent<Unit>();

        GameObject mageGO = Instantiate(magePrefab, mageBattleStation);
        mageUnit = mageGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<EnemyUnit>();

        if (secondEnemy)
        {
            GameObject enemy2GO = Instantiate(enemyPrefab, enemy2BattleStation);
            enemy2Unit = enemy2GO.GetComponent<EnemyUnit>();
        }

        if (thirdEnemy)
        {
            GameObject enemy3GO = Instantiate(enemyPrefab, enemy3BattleStation);
            enemy3Unit = enemy3GO.GetComponent<EnemyUnit>();
        }

        PlayerHUD.SetHUD(warriorUnit, mageUnit);
        EnemyHUD.SetEnemyHUD(enemyUnit);

        //Battle is set up so now its the players turn

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    void PlayerTurn()
    {
        Debug.Log("Players Turn. Choose an action");
    }

    void Player2Turn()
    {
        Debug.Log("Player2s Turn. Choose an action");
    }

    void EnemysTurn()
    {
        Debug.Log("Enemys are attacking!... not implemented yet");
    }

    void BattleWon()
    {
        Debug.Log("YOU WON!");
    }

    void BattleLost()
    {
        Debug.Log("YOU LOST! GAME OVER!");
    }

    public void OnAttackButton()
    {
        //Choose enemy to attack


        switch (state) 
        {
            case BattleState.PLAYERTURN:
                Debug.Log("Player 1 Attacking!");
                //Choose enemy
                StartCoroutine(PlayerAttack());
                break;
            case BattleState.PLAYER2TURN:
                Debug.Log("Player 2 Attacking!");
                StartCoroutine(PlayerAttack());
                break;
            default:
                Debug.Log("Its not your turn");
                return;//return nothing
        }
    }

    public void OnDefendButton()
    {
        switch (state)
        {
            case BattleState.PLAYERTURN:
                Debug.Log("Player 1 Defending!, Current Defense = " + warriorUnit.defense);
                StartCoroutine(PlayerDefend());
                break;
            case BattleState.PLAYER2TURN:
                Debug.Log("Player 2 Defending!, Current Defense = " + mageUnit.defense);
                StartCoroutine(PlayerDefend());
                Debug.Log("Player 2 Defended!, New Current Defense = " + mageUnit.defense);
                break;
            default:
                Debug.Log("Its not your turn");
                return;//return nothing
        }
    }

    IEnumerator PlayerAttack()
    {
        //Damage the enemy
        yield return new WaitForSeconds(2f);
        bool isDead = enemyUnit.TakeDamage(warriorUnit.attack);
        EnemyHUD.SetEnemyHUD(enemyUnit);

        //check if the enemy is dead
        if (isDead)
        {
            //If all the enemys are dead we can end it here
            Debug.Log("You WON!");
            state = BattleState.WON;
        }
        else
        {
            switch (state)
            {
                case BattleState.PLAYERTURN:
                    Debug.Log("Players Turn. Choose an action");
                    state = BattleState.PLAYER2TURN;
                    Player2Turn();
                    break;
                case BattleState.PLAYER2TURN:
                    Debug.Log("Player2s Turn. Choose an action");
                    state = BattleState.ENEMYSTURN;
                    EnemysTurn();
                    break;
                default:
                    Debug.Log("Its not your turn");
                    break;
            }
        }
    }

    IEnumerator PlayerDefend()
    {
        //Increase defense
        yield return new WaitForSeconds(2f);
        switch (state)
        {
            case BattleState.PLAYERTURN:
                warriorUnit.defense *= (int)1.5;
                Debug.Log("Player 1 Defended!, New Current Defense = " + warriorUnit.defense);
                state = BattleState.PLAYER2TURN;
                Player2Turn();
                break;
            case BattleState.PLAYER2TURN:
                Debug.Log("Player2s Turn. Choose an action");
                mageUnit.DefendBoost(mageUnit.defense);
                state = BattleState.ENEMYSTURN;
                EnemysTurn();
                break;
            default:
                Debug.Log("Its not your turn");
                break;
        }
    }
}
