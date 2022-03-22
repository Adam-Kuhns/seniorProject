using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private float displayTimer = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (displayTimer > 0)
            displayTimer -= Time.deltaTime;
        if(displayTimer <= 0)
            GameObject.Destroy(gameObject);
    }
}
