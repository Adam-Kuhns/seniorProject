using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject.Destroy(gameObject);
    }
}
