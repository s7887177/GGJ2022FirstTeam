using Feng.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 4.0f;
    public float minJumpVelocity = 3.0f;
    public float maxJumpVelocity = 6.0f;
    public float jumpAcceleration = 20.0f;
    public float attackDamage = 3.0f;
    public float attackColdTime = 0.5f;
    public float rollingColdTime = 1.0f;

    public AudioSource attackAudio;
    public AudioSource rollingAudio;

    
    bool isJumping;
    bool isMoving;
    bool isGounding;
    bool isRolling;
    public bool isFreezing;

    bool stopJumping;
    float rollingSpeed;
    float attackTime;
    float rollingTime;
    float stiffTime;
    [SerializeField] float jumpVelocity;

    Animator animator;
    Rigidbody2D rigidbody;

    public Collider2D rightAttackRange;
    public Collider2D leftAttackRange;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFreezing)
            return;

        rollingTime += Time.deltaTime;
        attackTime += Time.deltaTime;
        stiffTime += Time.deltaTime;

        if(isRolling){
            transform.Translate(rollingSpeed*Time.deltaTime, 0, 0);

            if(rollingTime > 0.3f)
                isRolling = false;
        }

        else if(stiffTime >= 0.0f){
            bool moveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
            bool moveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);

            if (isMoving = (moveRight ^ moveLeft)){
                transform.Translate(movementSpeed*(moveLeft ? -1 : 1)*Time.deltaTime, 0, 0);
                GetComponent<SpriteRenderer>().flipX = moveLeft;
            }

            if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && isMoving && !isJumping && rollingTime > rollingColdTime)
                Roll(moveLeft);
            
            else if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && isGounding)
                Jump();
            
            else if ((Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.Mouse0)) && attackTime > attackColdTime)
                Attack();

            if (moveLeft)
            {
                GetComponent<BattleUnit>().attackRange = leftAttackRange;
            }
            else if (moveRight)
            {
                GetComponent<BattleUnit>().attackRange = rightAttackRange;

            }
        }

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

        animator.SetBool("isRun", isMoving);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isFallDown", rigidbody.velocity.y < 0.0f);
    }

    void OnCollisionStay2D(Collision2D other){
        if(stiffTime > 0.0f && !isRolling)
            Physics2D.IgnoreLayerCollision(6,7,false);

        if (other.gameObject.tag == "Enemy" && stiffTime > 0.0f)
            TakeDamage(other.gameObject.GetComponent<BattleUnit>().Atk, true, true);
    
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
        attackAudio.Play();
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
        rollingAudio.Play();
        Physics2D.IgnoreLayerCollision(6,7,true);
    }

    public void TakeDamage(int damage, bool pushLeft = false, bool pushUp = false){
        GetComponent<BattleUnit>().OnDamage(damage);
        rigidbody.velocity = new Vector2(pushLeft ? -5.0f : 5.0f, rigidbody.velocity.y + (pushUp ? 5.0f : 0.0f));
        stiffTime = -0.5f;
        Physics2D.IgnoreLayerCollision(6,7,true);
    }

    void Dealth(){
        Debug.Log("died");
    }
}
