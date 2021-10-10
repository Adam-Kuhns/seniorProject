using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guyBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y == 0)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(-5, rb.velocity.y);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(5, rb.velocity.y);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, 5);
            }
        }
    }
}
