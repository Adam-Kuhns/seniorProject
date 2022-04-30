using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletBehavior : MonoBehaviour
{
    private Animator b_Animator;
    private Rigidbody2D rb;

    public GameObject bloodPrefab;

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
            //b_Animator.SetTrigger("hit");
            Instantiate(bloodPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy();
        } else {
            Destroy();
        }
    }

    void Destroy()
    {
        GameObject.Destroy(gameObject);
    }
}
