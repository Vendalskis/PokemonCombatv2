using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {
    //set stats needed for individual pokemon
    public int currentLevel;
    public int currentHP;
    public int maxHP;
    public int attackLevel;
    public string currentName;
    
    //subtract hp.
    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }
}
