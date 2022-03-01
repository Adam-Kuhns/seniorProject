using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator m_Animator;
    private GameObject cannonball;
    public GameObject cannonballPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts) {
          if(collision.gameObject.tag == "bullet")
          {
            Debug.Log("Shooting");
            m_Animator.SetTrigger("fire");
          }
          else{
          }
      }
    }
  
    void Shoot()
    {
        m_Animator.ResetTrigger("fire");
        GetComponent<AudioSource>().Play();

        if (transform.localScale.x > 0)
        {
            cannonball = Instantiate(cannonballPrefab, new Vector2(transform.position.x + 1f, transform.position.y + 0.2f), Quaternion.identity);
            cannonball.GetComponent<Rigidbody2D>().velocity = new Vector2(15, 0);
        }
        else
        {
            cannonball = Instantiate(cannonballPrefab, new Vector2(transform.position.x - 1f, transform.position.y + 0.2f), Quaternion.identity);
            cannonball.GetComponent<Rigidbody2D>().velocity = new Vector2(-15, 0);
        }
    }
}
