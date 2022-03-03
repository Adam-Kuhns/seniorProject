using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonballbehaviour : MonoBehaviour
{
    private Animator b_Animator;
    private Rigidbody2D rb;
    private GameObject collidedWith;

    // Start is called before the first frame update
    void Start()
    {
        b_Animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "enemy")
        {
            b_Animator.SetTrigger("explode");
            collidedWith = collision.gameObject;
        }else{
            GameObject.Destroy(gameObject);
            b_Animator.SetTrigger("explode");
        }


    }
    void KillEnemy(){
        GameObject.Destroy(gameObject);
        GameObject.Destroy(collidedWith);
    }
}
