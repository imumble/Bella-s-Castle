using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimedAttack : MonoBehaviour
{
    public BattleSystem battleSystem;
    public GameObject timedAttackBarGO;
    public Image PowerBarMask;
    public float barChangeSpeed = 1;
    public float maxPowerBarValue = 100;
    public float currentPowerBarValue;
    public bool PowerBarON;

    public void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            PowerBarON = false;
        }
    }

    public IEnumerator UpdatePowerBar()
    {
        while (PowerBarON)
        {
            currentPowerBarValue += barChangeSpeed;

            float fill = currentPowerBarValue / maxPowerBarValue;
            PowerBarMask.fillAmount = fill;
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }
}