using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float normalSpeed;
    public float sprintSpeed;
    public float moveSpeed;

    public bool isMoving;
    public bool canMove;
    private Vector2 input;

    private Animator animatior;

    public LayerMask solidObjectsLayer;

    public LayerMask tallGrassLayer;
    public int encounterChance;


    public Camera battleCam;

    public GameObject battle;
    

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        battleCam.enabled = false;
        //battle.SetActive(false);
        sprintSpeed = normalSpeed + 2;
        moveSpeed = normalSpeed;
        animatior = GetComponent<Animator>();
    
    }

    // Update is called once per frame
    void Update()
    {
        //for sprint
        if (Input.GetKey(KeyCode.LeftShift)&&canMove) moveSpeed = sprintSpeed;
        else moveSpeed = normalSpeed;

        if (!isMoving)
        {
            //Get input values
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            
            // Removes Diagonal Movement
            if (input.x != 0) input.y = 0;

            //for the movement
            if(input != Vector2.zero)
            {
                animatior.SetFloat("moveX", input.x);
                animatior.SetFloat("moveY", input.y);
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;
                //if true, call the coroutine "Move"
                if(IsWalkable(targetPos) && canMove)
                StartCoroutine(Move(targetPos));

            }

        }
        animatior.SetBool("isMoving", isMoving);


        if (Input.GetKeyDown(KeyCode.P))
        {
            battleCam.enabled = false;
            //battle.SetActive(false);
            canMove = true;
        }

    }

    //coroutine used for moving the player
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

        CheckForEncounters();
    }

    

    //Check if it is possible to move to the possition.
    private bool IsWalkable(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos, 0.15f, solidObjectsLayer) != null)
        {
            return false;
        }

        return true;
    }


    //Random pokemon spawns in tall grass
    void CheckForEncounters()
    {
        if (encounterChance > 100) encounterChance = 100;

        if (Physics2D.OverlapCircle(transform.position, 0.2f, tallGrassLayer) != null)
        {
            if(Random.Range(1,100 +1) <= encounterChance)
            {
                Pokemon pokemon = PokemonFactory.CreateRandom();
                Debug.Log("Encountered: " + pokemon.name);
                BattleEncounter();

            }
        }
    }

    public BattleSystem battleStart;
    void BattleEncounter()
    {
        canMove = false;
        
        //walkCam.enabled = false;
        //battle.SetActive(true);
        battleCam.enabled = true;

        StartCoroutine(battleStart.SetupBattle());

    }
}
