using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, PLAYER2TURN, ENEMYSTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public EnemySelect enemySelect;
    public TimedAttack timedAttack;

    public BattleState state;

    public GameObject warriorPrefab;
    public GameObject magePrefab;
    public GameObject enemyPrefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;

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
    public EnemyUnit enemyUnit;
    public EnemyUnit enemy2Unit;
    public EnemyUnit enemy3Unit;

    public bool regularAttack;

    private GameObject warriorGO;
    private GameObject mageGO;
    private GameObject enemyGO;
    private GameObject enemy2GO;
    private GameObject enemy3GO;
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;

        StartCoroutine(SetupBattle());
    }

    private void Update()
    {
        PlayerHUD.Player1Health.text = MainPlayer.playerCurrentHealth + "/" + MainPlayer.playerMaxHealth;
    }

    IEnumerator SetupBattle()
    {
        //for testing
        secondEnemy = true;
        thirdEnemy = true;

        Debug.Log("SetupBattle");
        warriorGO = Instantiate(warriorPrefab, warriorBattleStation);
        warriorUnit = warriorGO.GetComponent<Unit>();

        mageGO = Instantiate(magePrefab, mageBattleStation);
        mageUnit = mageGO.GetComponent<Unit>();

        enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<EnemyUnit>();

        if (secondEnemy)
        {
            enemy2GO = Instantiate(enemy2Prefab, enemy2BattleStation);
            enemy2Unit = enemy2GO.GetComponent<EnemyUnit>();
        }

        if (thirdEnemy)
        {
            enemy3GO = Instantiate(enemy3Prefab, enemy3BattleStation);
            enemy3Unit = enemy3GO.GetComponent<EnemyUnit>();
        }

        PlayerHUD.SetHUD(warriorUnit, mageUnit);
        EnemyHUD.SetEnemyHUD(enemyUnit);

        //Overworld attack takes place

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
        Debug.Log("Enemys Attack!");

        firstEnemyAttack();

        secondEnemyAttack();

        thirdEnemyAttack();

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    private void firstEnemyAttack()
    {
        //Is there a first enemy
        if (enemyUnit != null)
        {
            MainPlayer.playerCurrentHealth = MainPlayer.playerCurrentHealth - enemyUnit.attack;
            Debug.Log("MainplayerCurrentHealth" + MainPlayer.playerCurrentHealth);
        }
    }

    private void secondEnemyAttack()
    {
        //Is there a second enemy
        if (enemy2Unit != null)
        {
            MainPlayer.playerCurrentHealth = MainPlayer.playerCurrentHealth - enemy2Unit.attack;
            Debug.Log("MainplayerCurrentHealth" + MainPlayer.playerCurrentHealth);
        }
    }

    private void thirdEnemyAttack()
    {
        //is there third enemy
        if (enemy3Unit != null)
        {
            MainPlayer.playerCurrentHealth = MainPlayer.playerCurrentHealth - enemy3Unit.attack;
            Debug.Log("MainplayerCurrentHealth" + MainPlayer.playerCurrentHealth);
        }
    }

    void BattleWon()
    {
        Debug.Log("YOU WON!");
        SceneManager.LoadScene(2);
    }

    void BattleLost()
    {
        Debug.Log("YOU LOST! GAME OVER!");
    }

    public void OnAttackButton()
    {
        regularAttack = true;
        switch (state) 
        {
            case BattleState.PLAYERTURN:
                Debug.Log("Player 1 Attacking!");
                //Choose enemy
                enemySelect.ShowEnemySelectPanel();
                break;
            case BattleState.PLAYER2TURN:
                Debug.Log("Player 2 Attacking!");
                enemySelect.ShowEnemySelectPanel();
                break;
            default:
                Debug.Log("Its not your turn");
                return;//return nothing
        }
    }

    public void OnMagicButton()
    {
        regularAttack = false;
        switch (state)
        {
            case BattleState.PLAYERTURN:
                Debug.Log("Player 1 Attacking!");
                //Choose enemy
                enemySelect.ShowEnemySelectPanel();
                break;
            case BattleState.PLAYER2TURN:
                Debug.Log("Player 2 Attacking!");
                enemySelect.ShowEnemySelectPanel();
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

    public IEnumerator PlayerAttack(int enemySlot)
    {
        //Damage the enemy
        yield return new WaitForSeconds(2f);
        bool isDead = false;

        switch (enemySlot)
        {
            case 1:
                isDead = enemyUnit.TakeDamage(MainPlayer.playerATK);
                EnemyHUD.SetEnemyHUD(enemyUnit);
                break;
            case 2:
                isDead = enemy2Unit.TakeDamage(MainPlayer.playerATK);
                EnemyHUD.SetEnemyHUD(enemy2Unit);
                break;
            case 3:
                isDead = enemy3Unit.TakeDamage(MainPlayer.playerATK);
                EnemyHUD.SetEnemyHUD(enemy3Unit);
                break;
        }

        //check if the enemy is dead
        if (isDead)
        {
            //Destroy the enemy gameobject, and null the unit
            switch (enemySlot)
            {
                case 1:
                    Destroy(enemyGO);
                    enemyUnit = null;
                    break;
                case 2:
                    Destroy(enemy2GO);
                    enemy2Unit = null;
                    break;
                case 3:
                    Destroy(enemy3GO);
                    enemy3Unit = null;
                    break;
            }

            //check if there are any more bad guys
            if (enemyUnit == null && enemy2Unit == null && enemy3Unit == null)
            {
                //If all the enemys are dead we can end it here
                BattleWon();
            }
        }
        else //the fight goes on
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

    public IEnumerator PlayerTimedAttack(int enemySlot)
    {
        bool isDead = false;

        //Reset the timed attack bar
        timedAttack.currentPowerBarValue = 0;
        //show the timed attack bar
        timedAttack.timedAttackBarGO.SetActive(true);
        yield return new WaitForSeconds(1f);

        timedAttack.PowerBarON = true;
        StartCoroutine(timedAttack.UpdatePowerBar());
        yield return new WaitForSeconds(3f);
        timedAttack.PowerBarON = false;
        timedAttack.timedAttackBarGO.SetActive(false);

        if (timedAttack.PowerBarMask.fillAmount >= 0.45 && timedAttack.PowerBarMask.fillAmount <= 0.55)
        {
            switch (enemySlot)
            {
                case 1:
                    isDead = enemyUnit.TakeDamage(MainPlayer.playerATK * 2);
                    EnemyHUD.SetEnemyHUD(enemyUnit);
                    break;
                case 2:
                    isDead = enemy2Unit.TakeDamage(MainPlayer.playerATK * 2);
                    EnemyHUD.SetEnemyHUD(enemy2Unit);
                    break;
                case 3:
                    isDead = enemy3Unit.TakeDamage(MainPlayer.playerATK * 2);
                    EnemyHUD.SetEnemyHUD(enemy3Unit);
                    break;
            }
        }
        Debug.Log(timedAttack.PowerBarMask.fillAmount);
        
        //check if the enemy is dead
        if (isDead)
        {
            //Destroy the enemy gameobject, and null the unit
            switch (enemySlot)
            {
                case 1:
                    Destroy(enemyGO);
                    enemyUnit = null;
                    break;
                case 2:
                    Destroy(enemy2GO);
                    enemy2Unit = null;
                    break;
                case 3:
                    Destroy(enemy3GO);
                    enemy3Unit = null;
                    break;
            }

            //check if there are any more bad guys
            if (enemyUnit == null && enemy2Unit == null && enemy3Unit == null)
            {
                //If all the enemys are dead we can end it here
                BattleWon();
            }
        }
        else //the fight goes on
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
