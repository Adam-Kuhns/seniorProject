using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator m_Animator;
    private float bayonetRange = 1f;
    public Transform attackPoint;
    public Transform depthMeasure;

    private GameObject bullet;
    public GameObject bulletPrefab;

    public Transform Player;
    private int DetectionRange = 10;
    private int MinDist = 5;
    private bool isGrounded = false;
    private bool pitDetected = false;

    //public Text pointsBoard;
    public PointsBoardScript pointsBoard;

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
    void FixedUpdate()
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
        if (horizDistanceToPlayer <= DetectionRange && horizDistanceToPlayer >= MinDist && pitDetected == false)
        {
            float acceleration;
            if (isGrounded == true)
                acceleration = 0.7f;
            else
                acceleration = 0.3f;

            if (Player.position.x < transform.position.x)
            {
                if (rb.velocity.x > -4)
                {
                    rb.velocity = new Vector2(rb.velocity.x - acceleration, rb.velocity.y);
                }
                transform.localScale = new Vector2(1, 1);
                depthMeasure.localPosition = new Vector2(-1, 0);
                m_Animator.SetTrigger("Walk");
            }
            if (Player.position.x > transform.position.x)
            {
                if (rb.velocity.x < 4)
                {
                    rb.velocity = new Vector2(rb.velocity.x + acceleration, rb.velocity.y);
                }
                transform.localScale = new Vector2(-1, 1);
                depthMeasure.localPosition = new Vector2(-1, 0);
                m_Animator.SetTrigger("Walk");
            }
        }
        else
        {
            if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x - 0.3f, rb.velocity.y);
            }
            if (rb.velocity.x < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x + 0.3f, rb.velocity.y);
            }
        }
        if (rb.velocity.x == 0)
        {
            m_Animator.SetTrigger("StopWalk");
        }
        DepthMeasure();

        if (gunCooldownTimer > 0)
            gunCooldownTimer -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "bullet")
        {
            TakeDamage(1);
        }
        if(collider.gameObject.tag == "BottomlessPit")
        {
            GameObject.Destroy(gameObject);
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
                    if(contact.normal.y > 0)
                    {
                        isGrounded = true;
                    }
                    else if(isGrounded == true && (Mathf.Abs(contact.normal.x) > Mathf.Abs(contact.normal.y)))
                    {
                        // Horizontal Collision
                        rb.velocity = new Vector2(0, 7);
                        isGrounded = false;
                    }
                    break;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "enemy":
            case "Player":
            case "bullet":
            case "cannonball":
                break;
            default:
                isGrounded = false;
                break;
        }
    }

    void Shoot()
    {
        m_Animator.ResetTrigger("Shoot");
        GetComponent<AudioSource>().Play();

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

    void DepthMeasure()
    {
        LayerMask mask = LayerMask.GetMask("Tilemap");

        RaycastHit2D hit = Physics2D.Raycast(depthMeasure.position, -Vector2.up, Mathf.Infinity, mask);

        if (hit.collider != null)
        {
            pitDetected = false;
        }
        else
        {
            pitDetected = true;
        }
    }

    void Fly()
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 1.4f);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        m_Animator.SetTrigger("hurt");
        if(currentHealth <= 0)
        {
            pointsBoard.AddPoints(1000);
            m_Animator.SetTrigger("death");
        }
    }
}
