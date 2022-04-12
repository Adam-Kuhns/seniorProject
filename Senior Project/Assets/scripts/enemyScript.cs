using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HealthBar;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class enemyScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator m_Animator;
    private float attackRange = 1f;
    public Transform attackPoint;
    public GameObject pts100Prefab;
    public Transform Player;
    private int DetectionRange = 10;
    private int MinDist = 1;



    public Text pointsBoard;


    // Start is called before the first frame update
    void Start()
    {
        //points scoreboard
        pointsBoard = GameObject.Find("pointsBoard").GetComponent<Text>();

        rb = gameObject.GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, Player.position);
        if (distanceToPlayer <= MinDist)
        {
            m_Animator.SetTrigger("Attack");
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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "bullet")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Debug.Log("Dying");
            m_Animator.SetTrigger("Death");
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "cannonball")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Debug.Log("Dying");
            m_Animator.SetTrigger("CannonDeath");
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        bool isGrounded = false;
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
                    Debug.Log(contact.normal.x + " " + contact.normal.y);
                    if(contact.normal.y > 0)
                    {
                        isGrounded = true;
                    }
                    else if (isGrounded == true && (Mathf.Abs(contact.normal.x) > Mathf.Abs(contact.normal.y)))
                    {
                        // Horizontal Collision
                        rb.velocity = new Vector2(0, 9);
                    }
                    else
                    {
                        isGrounded = false;
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
            enemy.SendMessage("TakeDamage", 1);
        }
    }

    public void TakeDamage(int damage)
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Debug.Log("Dying");
        m_Animator.SetTrigger("Death");
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    void Destroy()
    {
        int scoreInt = 0;
        string currentScore = pointsBoard.text;
        int.TryParse(currentScore, out scoreInt);
        scoreInt = scoreInt + 100;
        pointsBoard.text = scoreInt.ToString();

        Instantiate(pts100Prefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        GameObject.Destroy(gameObject);
    }
}
