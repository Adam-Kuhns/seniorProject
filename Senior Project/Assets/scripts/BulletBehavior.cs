using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletBehavior : MonoBehaviour
{
    private Animator b_Animator;
    private Rigidbody2D rb;
    private GameObject collidedWith;

    void Start()
    {
        b_Animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "enemy")
        {
            rb.velocity = new Vector2(0, 0);
            b_Animator.SetTrigger("hit");
            collidedWith = collision.gameObject;
        }else{
          GameObject.Destroy(gameObject);
        }


    }

}
