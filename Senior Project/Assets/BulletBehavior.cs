using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "enemy")
            GameObject.Destroy(collision.gameObject);

        GameObject.Destroy(gameObject);
    }
}
