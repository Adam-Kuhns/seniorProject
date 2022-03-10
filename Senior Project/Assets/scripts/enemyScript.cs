using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HealthBar;
using UnityEngine.SceneManagement;

public class enemyScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator m_Animator;
    private float attackRange = 1f;
    public Transform attackPoint;
    public Transform Player;
    private int MinDist = 1;

    public guyBehaviourScript guy1;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, Player.position) <= MinDist)
        {
            m_Animator.SetTrigger("Attack");
        }
        if (Mathf.Abs(Player.position.x - transform.position.x) >= MinDist)
        {
            if (Player.position.x < transform.position.x)
            {
                if (rb.velocity.x > -4)
                {
                    rb.velocity = new Vector2(rb.velocity.x - 0.3f, rb.velocity.y);
                }
                transform.localScale = new Vector2(1, 1);
                m_Animator.SetTrigger("Walk");
            }
            if (Player.position.x > transform.position.x)
            {
                if (rb.velocity.x < 4)
                {
                    rb.velocity = new Vector2(rb.velocity.x + 0.3f, rb.velocity.y);
                }
                transform.localScale = new Vector2(-1, 1);
                m_Animator.SetTrigger("Walk");
            }
        }
        else
        {
            if(rb.velocity.x > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x - 0.3f, rb.velocity.y);
            }
            if(rb.velocity.x < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x + 0.3f, rb.velocity.y);
            }
        }
        if (rb.velocity.x == 0)
        {
            m_Animator.SetTrigger("StopWalk");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Debug.Log("Dying");
            m_Animator.SetTrigger("Death");
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if (collision.gameObject.tag == "cannonball")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Debug.Log("Dying");
            m_Animator.SetTrigger("CannonDeath");
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            switch (collision.gameObject.tag)
            {
                case "enemy":
                case "Player":
                case "bullet":
                case "cannonball":
                    break;
                default:
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
                    break;
            }
        }
    }

    void Attack()
    {
        m_Animator.ResetTrigger("Attack");
        LayerMask mask = LayerMask.GetMask("Players");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, mask);

        foreach (Collider2D enemy in hitEnemies)
        {
            guy1.TakeDamage(1);
            if(guy1.currentHealth <= 0)
            {

              //GameObject.Destroy(enemy.gameObject);
                SceneManager.LoadScene("DeathMenu");
            }
        }
    }

    void Destroy()
    {
        GameObject.Destroy(gameObject);
    }
}
