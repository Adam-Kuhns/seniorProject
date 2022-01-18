using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guyBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator m_Animator;
    private GameObject bullet;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponent<Animator>();
        //Time.timeScale = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y == 0)
        {
            if(m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                m_Animator.SetTrigger("StopJump");
            }
            if (Input.GetKey(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, 7);
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
                if (rb.velocity.x < 5)
                {
                    rb.velocity = new Vector2(rb.velocity.x + 0.3f, rb.velocity.y);
                }
                transform.localScale = new Vector2(1, 1);
                m_Animator.SetTrigger("Walk");
            }
            if (Input.GetKey(KeyCode.E))
            {
                m_Animator.SetTrigger("Shoot");
            }
        }
        else
        {
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
        }

        if (rb.velocity.x == 0)
        {
            m_Animator.SetTrigger("StopWalk");
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
}
