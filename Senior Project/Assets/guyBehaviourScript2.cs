using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HealthBar;

public class guyBehaviourScript2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator m_Animator;
    private float attackRange = 0.5f;
    public Transform attackPoint;
    public Transform Player;
    private int MinDist = 1;

    public int maxHealth = 10;
    public int currentHealth;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponent<Animator>();

        currentHealth = maxHealth;
        healthBar.Initialize();
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.F))
        if (Vector2.Distance(transform.position, Player.position) <= MinDist)
        {
            m_Animator.SetTrigger("Attack");
        }
        if (rb.velocity.y == 0)
        {
            if (Vector2.Distance(transform.position, Player.position) >= MinDist)
            {
                //if (Input.GetKey(KeyCode.LeftArrow))
                if (Player.position.x < transform.position.x)
                {
                    rb.velocity = new Vector2(-5, rb.velocity.y);
                    transform.localScale = new Vector2(1, 1);
                    m_Animator.SetTrigger("Walk2");
                }
                //if (Input.GetKey(KeyCode.RightArrow))
                if (Player.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(5, rb.velocity.y);
                    transform.localScale = new Vector2(-1, 1);
                    m_Animator.SetTrigger("Walk2");
                }
            }
            /*if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(rb.velocity.x, 7);
            }*/
        }
        if (rb.velocity.x == 0)
        {
            m_Animator.SetTrigger("StopWalk2");
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts) {
            if (Mathf.Abs(contact.normal.x) > Mathf.Abs(contact.normal.y))
            {
                // Horizontal Collision
                rb.velocity = new Vector2(rb.velocity.x, 7);
                if(contact.normal.x > 0)
                {
                    // Left Side Collision
                    rb.velocity = new Vector2(-1, rb.velocity.y);
                }
                if(contact.normal.x < 0)
                {
                    // Right Side Collision
                    rb.velocity = new Vector2(1, rb.velocity.y);
                }
            }
        }
    }

    void TakeDamage(int damage)
	   {
        currentHealth -= damage;

	       healthBar.SetHealth(currentHealth);
	   }

    void Attack()
    {
        m_Animator.ResetTrigger("Attack");
        LayerMask mask = LayerMask.GetMask("Players");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, mask);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log(enemy);
            TakeDamage(1);
            if(currentHealth <= 0)
            {
              GameObject.Destroy(enemy.gameObject);
            }
        }



    }
}
