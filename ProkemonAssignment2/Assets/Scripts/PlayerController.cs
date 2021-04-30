using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float normalSpeed;
    public float sprintSpeed;
    public float mSpeed;

    public bool isMoving;
    private Vector2 input;

    private Animator animatior;

    public LayerMask solidObjectsLayer;
    public LayerMask tallGrassLayer;

    // Start is called before the first frame update
    void Start()
    {
        sprintSpeed = normalSpeed + 2;
        mSpeed = normalSpeed;
        animatior = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(KeyCode.LeftShift)) mSpeed = sprintSpeed;
        else mSpeed = normalSpeed;

        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            // Removes Diagonal Movement
            if (input.x != 0) input.y = 0;

            if(input != Vector2.zero)
            {
                animatior.SetFloat("moveX", input.x);
                animatior.SetFloat("moveY", input.y);
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if(IsWalkable(targetPos))
                StartCoroutine(Move(targetPos));

            }

        }
        animatior.SetBool("isMoving", isMoving);
        
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, mSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

        CheckForEncounters();
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos, 0.15f, solidObjectsLayer) != null)
        {
            return false;
        }

        return true;
    }

    void CheckForEncounters()
    {
        if(Physics2D.OverlapCircle(transform.position, 0.2f, tallGrassLayer) != null)
        {
            if(Random.Range(1,100 +1) <= 25)
            {
                Debug.Log("Wild Pokemon encountered");
            }
        }
    }
}
