using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //reference to the battlesystem, for starting a battle.
    public BattleSystem battleStart;

    //Variables for controlling the move and sprint speed
    public float normalSpeed;
    public float sprintSpeed;
    public float moveSpeed;

    //Controlling movement
    public bool isMoving;
    public bool canMove;
    private Vector2 input;

    //reference to the animator
    private Animator animatior;

    public LayerMask solidObjectsLayer;

    //Check for tallgrass layer, used for pokemon encounter
    public LayerMask tallGrassLayer;
    //The cance for encounter, canged in the inspector
    public int encounterChance;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        sprintSpeed = normalSpeed + 2;
        moveSpeed = normalSpeed;
        animatior = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {        //for sprinting
        if (Input.GetKey(KeyCode.LeftShift) && canMove) moveSpeed = sprintSpeed;
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
                BattleEncounter();
            }
        }
    }

    // start the battle, called in CheckForEncounter
    void BattleEncounter()
    {
        canMove = false;
        StartCoroutine(battleStart.SetupBattle());
    }
}
