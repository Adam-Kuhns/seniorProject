using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guyBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float hDirection = Input.GetAxis("Horizontal");
        //Moving Left
        if (hDirection < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        //Moving Right
        else if (hDirection > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        if (rb.velocity.y == 0)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(-5, rb.velocity.y);
                m_Animator.SetTrigger("Walk");
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(5, rb.velocity.y);
                m_Animator.SetTrigger("Walk");
            }
            if (Input.GetKey(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, 5);
            }
        }
        if (rb.velocity.x == 0)
        {
            m_Animator.SetTrigger("StopWalk");
        }
    }
}
