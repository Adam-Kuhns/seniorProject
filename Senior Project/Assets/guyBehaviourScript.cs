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
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y == 0)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(-5, rb.velocity.y);
                transform.localScale = new Vector2(-1, 1);
                m_Animator.SetTrigger("Walk");
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(5, rb.velocity.y);
                transform.localScale = new Vector2(1, 1);
                m_Animator.SetTrigger("Walk");
            }
            if (Input.GetKey(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, 5);
            }
            if (Input.GetKey(KeyCode.E))
            {
                m_Animator.SetTrigger("Shoot");
            }
        }
        if (rb.velocity.x == 0)
        {
            m_Animator.SetTrigger("StopWalk");
        }
    }

    void Shoot()
    {
        if (transform.localScale.x > 0)
        {
            bullet = Instantiate(bulletPrefab, new Vector2(transform.position.x + 1f, transform.position.y + 0.2f), Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(20, 0);
        }
        else
        {
            bullet = Instantiate(bulletPrefab, new Vector2(transform.position.x - 1f, transform.position.y + 0.2f), Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-20, 0);
        }
    }
}
