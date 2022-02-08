using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HealthBar;


public class healthUp : MonoBehaviour
{
  public guyBehaviourScript guy1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        LayerMask mask = LayerMask.GetMask("Players");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("healing");
            guy1.HealDamage(10);
            GameObject.Destroy(gameObject);
        }
    }

}
