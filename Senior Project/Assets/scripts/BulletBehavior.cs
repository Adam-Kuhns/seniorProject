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

    void OnBecameInvisible()
    {
        Debug.Log("Invisible");
        Destroy();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "bullet":
            case "flag":
                break;
            case "enemy":
                rb.velocity = new Vector2(0, 0);
                Instantiate(bloodPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                Destroy();
                break;
            default:
                Destroy();
                break;
        }
    }

    void Destroy()
    {
        GameObject.Destroy(gameObject);
    }
}
