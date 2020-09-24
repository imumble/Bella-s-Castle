using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int attack;
    public int defense;

    public int maxHP;
    public int currentHP;
    public int maxMP;
    public int currentMP;


    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public void DefendBoost(int def)
    {
        double boostedDef = def * 1.5;
        defense = (int)boostedDef;
    }
}
