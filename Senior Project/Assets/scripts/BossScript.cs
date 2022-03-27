using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator m_Animator;
    private float bayonetRange = 1f;
    public Transform attackPoint;

    private GameObject bullet;
    public GameObject bulletPrefab;

    public Transform Player;
    private int DetectionRange = 10;
    private int MinDist = 5;

    private const float gunCooldownTime = 1;
    private float gunCooldownTimer = 0;

    public int maxHealth = 5;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Straight-line distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, Player.position);
        if (distanceToPlayer <= bayonetRange)
        {
            m_Animator.SetTrigger("Bayonet");
        }
        if (distanceToPlayer <= DetectionRange && gunCooldownTimer <= 0)
        {
            if (Player.position.x < transform.position.x)
            {
                transform.localScale = new Vector2(1, 1);
            }
            if (Player.position.x > transform.position.x)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            m_Animator.SetTrigger("Shoot");
            gunCooldownTimer = gunCooldownTime;
        }
        float horizDistanceToPlayer = Mathf.Abs(Player.position.x - transform.position.x);
        if (horizDistanceToPlayer <= DetectionRange && horizDistanceToPlayer >= MinDist)
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

        if (rb.velocity.x == 0)
        {
            m_Animator.SetTrigger("StopWalk");
        }

        if (gunCooldownTimer > 0)
            gunCooldownTimer -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "bullet")
        {
            TakeDamage(1);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0)
            {
                if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                {
                    m_Animator.SetTrigger("StopJump");
                }
            }
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
                        if (contact.normal.x > 0)
                        {
                            // Left Side Collision
                            rb.velocity = new Vector2(-1, rb.velocity.y);
                        }
                        if (contact.normal.x < 0)
                        {
                            // Right Side Collision
                            rb.velocity = new Vector2(1, rb.velocity.y);
                        }
                    }
                    break;
            }
        }
    }

    void Shoot()
    {
        m_Animator.ResetTrigger("Shoot");

        if (transform.localScale.x > 0)
        {
            bullet = Instantiate(bulletPrefab, new Vector2(transform.position.x - 1f, transform.position.y + 0.2f), Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-15, 0);
        }
        else
        {
            bullet = Instantiate(bulletPrefab, new Vector2(transform.position.x + 1f, transform.position.y + 0.2f), Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(15, 0);
        }
    }

    void Bayonet()
    {
        m_Animator.ResetTrigger("Attack");
        LayerMask mask = LayerMask.GetMask("Players");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, bayonetRange, mask);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.SendMessage("TakeDamage", 1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        m_Animator.SetTrigger("hurt");
        if(currentHealth <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
