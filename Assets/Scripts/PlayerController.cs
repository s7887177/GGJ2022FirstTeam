using Feng.Battle;
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
    public float attackTime;
    public float attackColdTime;
    public float rollingColdTime;
    public float health;

    public bool isJumping;
    public bool isMoving;
    public bool isGounding;
    public bool isRolling;

    bool stopJumping;
    float rollingSpeed;
    [SerializeField] float rollingTime;
    [SerializeField] float jumpVelocity;


    public AudioSource audioSource;
    Animator animator;
    Rigidbody2D rigidbody;

    public Collider2D rightAttackRange;
    public Collider2D leftAttackRange;


    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 4.0f;
        minJumpVelocity = 6.0f;
        maxJumpVelocity = 7.0f;
        jumpAcceleration = 15.0f;
        attackDamage = 10.0f;
        attackColdTime = 1.0f;
        rollingColdTime = 1.0f;

        isGounding = isMoving = isJumping = stopJumping = false;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        rollingTime += Time.deltaTime;
        attackTime += Time.deltaTime;

        if (isRolling)
        {
            transform.Translate(rollingSpeed * Time.deltaTime, 0, 0);

            if (rollingTime > 0.3f)
                isRolling = false;
        }
        else
        {
            bool moveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
            bool moveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);

            if (isMoving = (moveRight ^ moveLeft))
            {
                transform.Translate(movementSpeed * (moveLeft ? -1 : 1) * Time.deltaTime, 0, 0);
                GetComponent<SpriteRenderer>().flipX = moveLeft;
            }

            if (Input.GetKey(KeyCode.S) && isMoving && !isJumping && rollingTime > rollingColdTime)
                Roll(moveLeft);

            else if (Input.GetKey(KeyCode.Space) && isGounding)
                Jump();

            else if (Input.GetKey(KeyCode.J) && attackTime > attackColdTime)
                Attack();

            if (isJumping)
            {  // Jumping Process
                if (!stopJumping)
                {
                    rigidbody.velocity = new Vector2(0, jumpVelocity += jumpAcceleration * Time.deltaTime);
                    stopJumping = (jumpVelocity >= maxJumpVelocity || !Input.GetKey(KeyCode.Space));
                }
                else if (isGounding)
                    isJumping = stopJumping = false;
            }
            if (moveLeft)
            {
                GetComponent<BattleUnit>().attackRange = leftAttackRange;
            }
            else if (moveRight)
            {
                GetComponent<BattleUnit>().attackRange = rightAttackRange;

            }
        }

        animator.SetBool("isRun", isMoving);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isFallDown", rigidbody.velocity.y <= 0.0f);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (!isGounding && rigidbody.velocity == new Vector2(0.0f, 0.0f))
        {   // Gounded
            isGounding = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        isGounding = (rigidbody.velocity == new Vector2(0.0f, 0.0f));
    }



    void Attack()
    {
        animator.SetTrigger("Attack");
        attackTime = 0.0f;
        GetComponent<BattleUnit>().PerformAttack();
    }

    void Jump()
    {
        jumpVelocity = minJumpVelocity;
        isJumping = true;
        isGounding = false;
    }

    void Roll(bool moveLeft)
    {
        animator.SetTrigger("Roll");
        isRolling = true;
        rollingSpeed = movementSpeed * (moveLeft ? -3 : 3);
        rollingTime = 0.0f;
    }
}
