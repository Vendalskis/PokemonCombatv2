using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatHud : MonoBehaviour {

   
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;

    public void SetHUD(Stats stats)
    {
        nameText.text = stats.currentName;
        levelText.text = "Lvl " + stats.currentLevel;
        hpSlider.maxValue = stats.maxHP;
        hpSlider.value = stats.currentHP;
        //hpText.value = stats.currentHP;
        //maxHPText.text = stats.maxHP;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;

    }
}
