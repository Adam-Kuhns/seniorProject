using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guyBehaviourScript2 : MonoBehaviour
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
      if (Input.GetKey(KeyCode.F))
      {
          m_Animator.SetTrigger("Attack");
      }
        if (rb.velocity.y == 0)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector2(-5, rb.velocity.y);
                m_Animator.SetTrigger("Walk2");
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = new Vector2(5, rb.velocity.y);
                m_Animator.SetTrigger("Walk2");
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(rb.velocity.x, 7);
            }
        }
        if (rb.velocity.x == 0)
        {
            m_Animator.SetTrigger("StopWalk2");
        }
    }

    void Attack()
    {
        m_Animator.ResetTrigger("Attack");
    }
}
