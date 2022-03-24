using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletBehavior : MonoBehaviour
{
    private Animator b_Animator;
    private Rigidbody2D rb;

    void Start()
    {
        b_Animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "bullet")
        {
            
        }
        if(collider.gameObject.tag == "enemy")
        {
            rb.velocity = new Vector2(0, 0);
            b_Animator.SetTrigger("hit");
        } else {
            Destroy();
        }
    }

    void Destroy()
    {
        GameObject.Destroy(gameObject);
    }
}
