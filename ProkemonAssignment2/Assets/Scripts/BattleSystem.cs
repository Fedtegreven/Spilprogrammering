using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//enum for controlling the different states of the game.
public enum BattleState{START, PLAYERTURN, ENEMYTURN, WON, LOSS}
public class BattleSystem : MonoBehaviour
{
    //The camera that displays the battle.
    [SerializeField] Camera battleCam;

    //The player and enemy pokemon (Serialized so that we can view them in the inspector)
    [SerializeField] Pokemon playerPokemon;
    [SerializeField] Pokemon enemyPokemon;

    //List of the move buttons.
    [SerializeField] List<Button> moveset;

    //reference for the dialougetext
    [SerializeField] Text dialogText;
    
    //References for the player and enemyhuds
    [SerializeField] Text playerName;
    [SerializeField] Text playerLvl;
    [SerializeField] Slider playerHealthbar;
    [SerializeField] Text playerType;

    [SerializeField] Text enemyName;
    [SerializeField] Text enemyLvl;
    [SerializeField] Slider enemyHealtbar;
    [SerializeField] Text enemyType;

    //For implmenting images for the respective pokemons (Not implemneted) 
    [SerializeField] Image playerVisual;
    [SerializeField] Image enemyVisual;


    public BattleState state;



    // Start is called before the first frame update
    void Start()
    {
        battleCam.enabled = false;

        //has become somewhat obsolete.. 
        state = BattleState.START;
              
    }

    //setup all the information of the start of battle
   public IEnumerator SetupBattle()
    {
       state = BattleState.START;

       battleCam.enabled = true;
       playerPokemon = PokemonFactory.Create(5, "Charizard");
        //enemyPokemon = PokemonFactory.CreateRandom();
        enemyPokemon = PokemonFactory.CreateRandom();
        //Added more moves to the player for testing
        Move move2 = new Move("Fire Stuff");
        Move move3 = new Move("Fire Blast");
        Move move4 = new Move("Fly");
        List<Move> MovesToadd = new List<Move> {move2,move3, move4};
        playerPokemon.moves.AddRange(MovesToadd);

        dialogText.text = "A crazy " + enemyPokemon.name + " appeared";

        SetHud();
        AssingMoves();

        yield return new WaitForSeconds(1.5f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }


    //Assign the move names of the player pokemon to the Move buttons
    void AssingMoves()
    {
        for (int i = 0; i < playerPokemon.moves.Count; i++)
        {
            moveset[i].GetComponentInChildren<Text>().text = playerPokemon.moves[i].name;
        }
    }

    //Setup the hud of the player and enemy, hp, lvl, ect.
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

        playerType.text = playerPokemon.element.ToString();
        enemyType.text = enemyPokemon.element.ToString();
    }

    //When it's the players turn display this message
    void PlayerTurn()
    {
        dialogText.text = playerPokemon.name + "'s Turn. Choose a move!";
    }

    //Run wwhen the battle is over, loss or win.
    IEnumerator BattleOver()
    {
        if (state == BattleState.WON)
        {
            //Wait for this coroutine to finish. (display the attackt he player used)
            yield return PlayerAttackText();
            dialogText.text = playerPokemon.name + " Won the Battle!!!";
        }

        else if (state == BattleState.LOSS) dialogText.text = playerPokemon.name + " Faints"+" you lost..";

        yield return new WaitForSeconds(1.5f);

        StartCoroutine(EndScene());
    }

    //The enemyes turn..Lots of text, apply dmg to the player, and change state/ method denpending on the outcome.
    IEnumerator Enemyturn()
    {

        yield return PlayerAttackText();

        dialogText.text = "Enemy Turn..";

        yield return new WaitForSeconds(1f);

        //store the dmg value in an int, to display + damages the player.
        int damage = enemyPokemon.Attack(playerPokemon);
        dialogText.text = enemyPokemon.name + " Used " + enemyPokemon.moves[Random.Range(0,enemyPokemon.moves.Count)].name + " for " + damage + " DMG";

        playerHealthbar.value = playerPokemon.hp;

        yield return new WaitForSeconds(2f);

        //if player is still alive set the game to players turn
        if (playerPokemon.hp > 0) { 
            state = BattleState.PLAYERTURN; 
            PlayerTurn(); 
        }
        else
        {
            //if player is <=0 set state to 0 and start battleOver coroutine.
            state = BattleState.LOSS;
            StartCoroutine(BattleOver());
        }
    }

    //Assigned to the different Atack moves in the canvas, Run when one of those buttons is pressed.
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

    //Assigned to the heal button, calls a Coroutine if it's pressedn and it's the players turn.
    public void OnhealEnter()
    {
        if (state == BattleState.PLAYERTURN)
        {
            StartCoroutine(Heal());

        }
        else return;
    }


    //the coroutine called in OnhealEnter..restores the player pokemon to fullm health and set the state to enemy turn.
    IEnumerator Heal()
    {
        dialogText.text = "You have been healed! From: " + playerPokemon.hp + " HP to "+ playerPokemon.maxHp + " HP"; 
        playerPokemon.Restore();
        playerHealthbar.value = playerPokemon.hp;
        yield return new WaitForSeconds(1f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(Enemyturn());
    }


    // Makes it possible to run from the battle, assigned to the run button. calls the coroutine EndScene.
    public void OnRunEneter()
    {
        if (state == BattleState.PLAYERTURN)
        {
            dialogText.text = "You ran away...";
            StartCoroutine(EndScene());
        }
        else return;
    }

    // used when the battle cam should be disabled. Also makes it possible to move the player again.
    public IEnumerator EndScene()
    {
        yield return new WaitForSeconds(1f);        
            battleCam.enabled = false;
            //Reference to the PlayerController, to enable movement again.
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerController pc = player.GetComponent<PlayerController>();
            pc.canMove = true;
    }


    //Coroutine for getting the text of the buttons pressed, and then display the name of the move used.
    IEnumerator PlayerAttackText()
    {
        string moveName = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        int damage = playerPokemon.DamageDone(enemyPokemon);
        dialogText.text = playerPokemon.name + " Used " + moveName + " For " + damage + " DMG";

        yield return new WaitForSeconds(2f);
    }
}
