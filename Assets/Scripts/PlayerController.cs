using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float minJumpVelocity;
    public float maxJumpVelocity;
    public float jumpAcceleration;
    public float attackDamage;
    public float attackSpeed;   // second
    public float health;
    public bool isJumping;
    public bool isMoving;
    public bool isGounding;
    bool stopJumping;
    [SerializeField] float jumpVelocity;


    public Animator animator;
    public AudioSource audioSource;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 5.0f;
        minJumpVelocity = 3.0f;
        maxJumpVelocity = 5.0f;
        jumpAcceleration = 15.0f;
        attackDamage = 3.0f;
        attackSpeed = 15.0f;


        isGounding = isMoving = isJumping = stopJumping = false;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bool moveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool moveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);

        if (isMoving = (moveRight ^ moveLeft))
        {
            if(moveRight)
            {
                transform.Translate(movementSpeed*Time.deltaTime, 0, 0);
            }
            else if(moveLeft)
            {
                transform.Translate(-movementSpeed*Time.deltaTime, 0, 0);
            }
            GetComponent<SpriteRenderer>().flipX = moveLeft;
        }

        if (Input.GetKey(KeyCode.Space) && isGounding)
        { // Jump
            jumpVelocity = minJumpVelocity;
            isJumping = true;
            isGounding = false;
        }
        if (Input.GetKey(KeyCode.J))
        { // Attack
        }



        if(isJumping)
        {  // Jumping Process
            if(!stopJumping){
                stopJumping = (jumpVelocity >= maxJumpVelocity || !Input.GetKey(KeyCode.Space) || rigidbody.velocity.y <= 0.0f);
                rigidbody.velocity = new Vector3(0, jumpVelocity += jumpAcceleration*Time.deltaTime, 0);
            }
            else if(rigidbody.velocity.y == 0.0f)
                isJumping = stopJumping = false;
        }

        animator.SetBool("isRun", isMoving);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isFallDown", rigidbody.velocity.y <= 0.0f);
    }

    void OnCollisionStay(Collision other){
        if (!isGounding && rigidbody.velocity == new Vector3(0.0f, 0.0f ,0.0f))
        {   // Gounded
            isGounding = true;
        }
    }

    void OnCollisionExit(Collision other){
        isGounding = (rigidbody.velocity == new Vector3(0.0f, 0.0f ,0.0f));
    }
}
