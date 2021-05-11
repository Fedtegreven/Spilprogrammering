using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//enum for controlling the state of the game.
public enum BattleState{START, PLAYERTURN, ENEMYTURN, WON, LOSS}
public class BattleSystem : MonoBehaviour
{
    [SerializeField] Camera battleCam;

    [SerializeField] Pokemon playerPokemon;
    [SerializeField] Pokemon enemyPokemon;

    [SerializeField] List<Button> moveset;

    [SerializeField] Text dialogText;

    [SerializeField] Text playerName;
    [SerializeField] Text playerLvl;
    [SerializeField] Slider playerHealthbar;

    [SerializeField] Text enemyName;
    [SerializeField] Text enemyLvl;
    [SerializeField] Slider enemyHealtbar;

    [SerializeField] Image playerVisual;
    [SerializeField] Image enemyVisual;


    public BattleState state;



    // Start is called before the first frame update
    void Start()
    {
        
       // state = BattleState.START;
       // StartCoroutine(SetupBattle());        
    }

   public IEnumerator SetupBattle()
    {
        state = BattleState.START;

       playerPokemon = PokemonFactory.Create(5, "Charizard");
       enemyPokemon = PokemonFactory.CreateRandom();

        Move newMove;
        newMove = new Move("Weee");

        playerPokemon.moves.Add(newMove);



       dialogText.text = "A crazy " + enemyPokemon.name + " appeared";

        SetHud();
        AssingMoves();

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }


    void AssingMoves()
    {
        /*foreach (Button moveText in moveset)
        {
            foreach (Move item in playerPokemon.moves)
            {
                if (playerPokemon.moves.Count >= moveset.Count)
                    moveText.GetComponentInChildren<Text>().text = item.name;
                else return;
            }
        }*/

        /*foreach (Move item in playerPokemon.moves)
        {
            foreach (Button itemButton in moveset)
            {
                itemButton.GetComponentInChildren<Text>().text = item.name;
            }
        }*/

        for (int i = 0; i < playerPokemon.moves.Count; i++)
        {
            moveset[i].GetComponentInChildren<Text>().text = playerPokemon.moves[i].name;
        }
    }

    void SetHud()
    {
        playerName.text = playerPokemon.name;
        enemyName.text = enemyPokemon.name;

        playerLvl.text = "Lvl " + playerPokemon.level.ToString();
        enemyLvl.text = "Lvl " + enemyPokemon.level.ToString();

        playerHealthbar.maxValue = playerPokemon.maxHp;
        enemyHealtbar.maxValue = enemyPokemon.maxHp;

        playerHealthbar.value = playerPokemon.hp;
        enemyHealtbar.value = enemyPokemon.hp;
    }

    void PlayerTurn()
    {
        dialogText.text = "Choose a move!";
    }

    IEnumerator BattleOver()
    {
        if (state == BattleState.WON)
        {
            dialogText.text = playerPokemon.name + " Won the Battle!!!";
        }

        else if (state == BattleState.LOSS) dialogText.text = "You lost " + enemyPokemon.name + " won the battle.....";

        yield return new WaitForSeconds(3f);

        dialogText.text = "It's Over";
        
            //battleCam.enabled = false;
    }

    IEnumerator Enemyturn()
    {
        dialogText.text = enemyPokemon.name + " Attacked!!!";

        yield return new WaitForSeconds(1f);
        enemyPokemon.Attack(playerPokemon);

        playerHealthbar.value = playerPokemon.hp;

        yield return new WaitForSeconds(2f);

        if (playerPokemon.hp > 0) { 
            state = BattleState.PLAYERTURN; 
            PlayerTurn(); 
        }
        else
        {
            state = BattleState.LOSS;
            StartCoroutine(BattleOver());
        }
    }

    public void OnAttackButton()
    {
        if (state == BattleState.PLAYERTURN)
        {
            playerPokemon.Attack(enemyPokemon);

            enemyHealtbar.value = enemyPokemon.hp;
            if (enemyPokemon.hp > 0)
            {
                state = BattleState.ENEMYTURN;

                StartCoroutine(Enemyturn());
            }

            else
            {
                state = BattleState.WON;
                StartCoroutine(BattleOver());
            }

        }
        else return;
    }

    public void OnhealEnter()
    {
        if (state == BattleState.PLAYERTURN)
        {
            playerPokemon.Restore();
            playerHealthbar.value = playerHealthbar.maxValue;
            state = BattleState.ENEMYTURN;
            Enemyturn();
        }
        else return;

    }
}
