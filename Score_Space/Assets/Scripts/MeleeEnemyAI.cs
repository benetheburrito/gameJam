using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAI : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool facingRight = true;
    public GameObject player;
    public float movementRange; 

    private float moveInput;
    private float speed;
    public float health = 3;

    public float attackCooldown;
    private float nextAttackTime;

    public Transform hitbox;
    public float attackRange = .5f;
    public LayerMask playerLayer;


    //private bool isGrounded;
    //public Transform groundCheck;
    //public float checkRadius;
    //public LayerMask whatIsGround;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 5;
        nextAttackTime = 0;

    }

    private void FixedUpdate()
    {

        if (facingRight)
        {
            moveInput = 1;
        }
        else
        {
            moveInput = -1;
        }

        
        if ((player.transform.position.x - rb.position.x) > .01)
        {
            if (facingRight == false)
            {
                flip();
            }
        }
        else if ((player.transform.position.x - rb.position.x) < -.01)
        {
            if (facingRight)
            {
                flip();
            }
        }
        
        if (Mathf.Abs(player.transform.position.x - rb.position.x) < attackRange)
        {
            moveInput = 0;
            if (Time.time > nextAttackTime)
            {
                nextAttackTime = Time.time + attackCooldown;
                //Debug.Log("Time: " + Time.time + "Next Attack: " + nextAttackTime);
                attack();
                
            }
        }
        if(Mathf.Abs(player.transform.position.x - transform.position.x) + Mathf.Abs(player.transform.position.y - transform.position.y) < movementRange)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y); 
            moveInput = 0;
        }
        
        animator.SetFloat("AnimationSpeed", Mathf.Abs(moveInput));
    }

    void attack()
    {
        animator.SetTrigger("Attack1");
        Collider2D hit = Physics2D.OverlapCircle(hitbox.position, attackRange, playerLayer);
        //Debug.Log(hit.name + "test");
        hit.gameObject.GetComponent<PlayerHealth>().getHit(1);
    }


    void flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }

    public void die()
    {
        transform.position += new Vector3(0, -.75f, 0);
        this.GetComponent<Rigidbody2D>().gravityScale = 0f; 
    }
    
    public void getHit(int damage)
    {
        health -= damage;
        //Debug.Log("took " + damage + "damage");
    }
}