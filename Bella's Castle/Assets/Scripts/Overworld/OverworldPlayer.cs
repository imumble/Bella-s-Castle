using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPlayer : MonoBehaviour
{
    static bool enemy1;
    static bool enemy2;
    static bool enemy3;

    public Collider badGuySensor;

    Dictionary<int, EnemyUnit> JoeyDic = new Dictionary<int, EnemyUnit>();

    private void Update()
    {
        switch (JoeyDic.Keys.Count)
        {
            case 1:
                enemy1 = true;
                break;
            case 2:
                enemy2 = true;
                break;
            case 3:
                enemy3 = true;
                break;
            //shouldnt ever happen
            default:
                //no enemys
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triger enter " + other.gameObject.name);
        //JoeyDic.Add(1, other.gameObject.GetComponent<EnemyUnit>());
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Enemy Left!");
        //JoeyDic.Remove(1);
    }
}
