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
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector2(0, 5);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-5, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(5, 0);
        }
    }
}
