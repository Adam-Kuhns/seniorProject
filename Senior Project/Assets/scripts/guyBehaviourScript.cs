using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class guyBehaviourScript : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator m_Animator;
    private GameObject bullet;
    public GameObject bulletPrefab;
    private float attackRange = 1f;
    public Transform attackPoint;

    //Jump boost powerup
    private float jump = 7; //value used in the translate function
    private float normaljump = 7; //default jump height
    private float jumpUp = 12; //jump height when the powerup is collected
    private float jumpUpDuration = 10; // how long the powerup lasts

    private const float gunCooldownTime = 1;
    private float gunCooldownTimer = 0;

    public int maxHealth = 10;
    public int currentHealth;

    public HealthBar healthBar;

    //Sound stuff
    public AudioClip jumpSound;
    public AudioClip pokeSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponent<Animator>();

        //Time.timeScale = 0.25f;
        currentHealth = maxHealth;
        healthBar.Initialize();
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && gunCooldownTimer <= 0)
        {
            m_Animator.SetTrigger("Shoot");
            gunCooldownTimer = gunCooldownTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (rb.velocity.x > -5)
            {
                rb.velocity = new Vector2(rb.velocity.x - 0.1f, rb.velocity.y);
            }
            transform.localScale = new Vector2(-1, 1);
            m_Animator.SetTrigger("Walk");
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (rb.velocity.x < 5)
            {
                rb.velocity = new Vector2(rb.velocity.x + 0.1f, rb.velocity.y);
            }
            transform.localScale = new Vector2(1, 1);
            m_Animator.SetTrigger("Walk");
        }

        if (rb.velocity.x == 0)
        {
            m_Animator.SetTrigger("StopWalk");
        }

        if(gunCooldownTimer > 0)
            gunCooldownTimer -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "BottomlessPit")
        {
            SceneManager.LoadScene("DeathMenu");
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
                if (Input.GetKey(KeyCode.Space))
                {
                    rb.velocity = new Vector2(rb.velocity.x, jump);
                    m_Animator.SetTrigger("Jump");
                }
                if (Input.GetKey(KeyCode.A))
                {
                    if (rb.velocity.x > -5)
                    {
                        rb.velocity = new Vector2(rb.velocity.x - 0.3f, rb.velocity.y);
                    }
                    transform.localScale = new Vector2(-1, 1);
                    m_Animator.SetTrigger("Walk");
                }
                if (Input.GetKey(KeyCode.D))
                {
                    if(rb.velocity.x < 5)
                    {
                        rb.velocity = new Vector2(rb.velocity.x + 0.3f, rb.velocity.y);
                    }
                    transform.localScale = new Vector2(1, 1);
                    m_Animator.SetTrigger("Walk");
                }
                if (Input.GetKey(KeyCode.F))
                {
                    m_Animator.SetTrigger("Bayonet");
                }
            }
        }
    }

    void Shoot()
    {
        m_Animator.ResetTrigger("Shoot");
        GetComponent<AudioSource>().Play();

        if (transform.localScale.x > 0)
        {
            bullet = Instantiate(bulletPrefab, new Vector2(transform.position.x + 1f, transform.position.y + 0.2f), Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(15, 0);
        }
        else
        {
            bullet = Instantiate(bulletPrefab, new Vector2(transform.position.x - 1f, transform.position.y + 0.2f), Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-15, 0);
        }
    }

    void Jump()
    {
        GetComponent<AudioSource>().PlayOneShot(jumpSound);
    }

    void Poke()
    {
        GetComponent<AudioSource>().PlayOneShot(pokeSound);
    }

    void Bayonet()
    {
        m_Animator.ResetTrigger("Bayonet");
        LayerMask mask = LayerMask.GetMask("Enemies");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, mask);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log(enemy);
            GameObject.Destroy(enemy.gameObject);
        }
    }

    public void TakeDamage(int damage)
	{
        currentHealth -= damage;

	    healthBar.SetHealth(currentHealth);
	}

    public void HealDamage(int heal)
    {
        currentHealth = maxHealth;

        healthBar.SetHealth(currentHealth);
    }

    public void ActivateJumpBoost()
    {
        StartCoroutine(JumpBoostCooldown());
    }

    IEnumerator JumpBoostCooldown()
    {
        jump = jumpUp;
        yield return new WaitForSeconds(jumpUpDuration);
        jump = normaljump;
    }
}
