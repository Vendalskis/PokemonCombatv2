using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TurnState { START, PLAYERTURN, ENEMYTURN, WON, LOST, RUN, BAG, POKEMON }

public class TurnSystem : MonoBehaviour {

    
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleZone;
    public Transform enemyBattleZone;

    Stats playerPoke;
    Stats enemyPoke;

    public Text CombatText;

    public CombatHud PlayerBar;
    public CombatHud EnemyBar;

    public TurnState state;

    void Start()
    {
        //begin in the starting state
        state = TurnState.START;
        //set up the battle
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        //spawn player and enemy in respective locations
        GameObject playerGameObject = Instantiate(playerPrefab, playerBattleZone);
        playerPoke = playerGameObject.GetComponent<Stats>();

        GameObject enemyGameObject = Instantiate(enemyPrefab, enemyBattleZone);
        enemyPoke = enemyGameObject.GetComponent<Stats>();
        //add text of wild pokemon and corresponding name
        CombatText.text = "A wild " + enemyPoke.currentName + " appears!";
        //update UI
        PlayerBar.SetHUD(playerPoke);
        EnemyBar.SetHUD(enemyPoke);
        //wait 2 seconds
        yield return new WaitForSeconds(2f);
        //change state to players turn
        state = TurnState.PLAYERTURN;
        PlayerTurn();
    }


    IEnumerator PlayerAttack()
    {
        bool isDead = enemyPoke.TakeDamage(playerPoke.attackLevel);
        //Damage the enemy
                //need to play sound of player taking damage
                //need play animation of player taking damage, red flash or something or fade in and out of sprite.
        EnemyBar.SetHP(enemyPoke.currentHP);
        CombatText.text = "The move hits!";

        yield return new WaitForSeconds(2f);
        //if enemy is at 0 or less than 0, win state, run end battle. 
        if(isDead)
        {
            state = TurnState.WON;
            EndBattle();
            //if enemy is alive, start enemies turn
        } else
        {
            state = TurnState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        //for simplicities sake, the opponent will only use tackle on its turns.
        CombatText.text = enemyPoke.currentName + " uses Tackle!";

        yield return new WaitForSeconds(1f);

        
        //play sound of player taking damage
        //play animation of player taking damage, red flash or something or fade in and out of sprite.

        bool isDead = playerPoke.TakeDamage(enemyPoke.attackLevel);

        PlayerBar.SetHP(playerPoke.currentHP);

        yield return new WaitForSeconds(1f);
        //if you die, lose state and start endbattle
        if(isDead)
        {
            state = TurnState.LOST;
            EndBattle();

        } else//if not dead, start players turn
        {
            state = TurnState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator PlayerRun()
    {
        CombatText.text = "You Got Away Safely";
        //play running away noise
        yield return new WaitForSeconds(2f);
        state = TurnState.RUN;//changing the state can make sure you don't swap actions during turn.
    }

    IEnumerator PlayerPokemonScreen()
    {
        CombatText.text = "Swapping to other pokemon";
        //enable new canvas and disable old canvas
        //play running away noise
        yield return new WaitForSeconds(2f);
        state = TurnState.POKEMON;

    }

    IEnumerator PlayerBag()
    {
        //swap to bag, give delay, change canvas to a new bag UI.
        CombatText.text = "Swapping to your bag";
        //enable the new canvas for player bag and disable old canvas
        yield return new WaitForSeconds(2f);
        state = TurnState.BAG;
    }

    void EndBattle()
    {
        //if you bring enemy to 0 hp, go to win state
        if(state == TurnState.WON)
        {
            CombatText.text = "You've defeated the wild " + enemyPoke.currentName;
        } else if (state == TurnState.LOST) //if you went to 0 hp, begin lose state.
        {
            CombatText.text = "You were defeated by the wild " + enemyPoke.currentName;
        }else if (state == TurnState.RUN)//if you wanted to run away, message that you ran away
            {
            CombatText.text = "You ran away safely";
        }
    }


    void PlayerTurn()
    {
        //text to choose next action
        CombatText.text = "Choose an action:";

    }

    public void OnCombatButton()
    {
        //if fighting, begin fighting coroutine
        if (state != TurnState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnRunButton()
    {
        //if using run button, do the coroutine
        if (state != TurnState.PLAYERTURN)
            return;

        StartCoroutine(PlayerRun());
    }

     public void OnBagButton()
    {
        //if using bag button do coroutine
        if (state != TurnState.PLAYERTURN)
            return;

        StartCoroutine(PlayerBag());
    }

    public void OnPokemonButton()
    {
        //if using pokemon button, do coroutine
        if (state != TurnState.PLAYERTURN)
            return;

        StartCoroutine(PlayerPokemonScreen());
    }


    public void OnFightButton()
    {
        
        
    }
}
