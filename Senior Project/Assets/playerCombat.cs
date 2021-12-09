using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{

    public Transform attackPoint;
    public float attackRange = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
          Attack();
        }

        void Attack()
        {
          LayerMask mask = LayerMask.GetMask("Players");
          Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, mask);

          foreach (Collider2D enemy in hitEnemies)
          {
            Debug.Log(enemy);
            DestroyImmediate(enemy.gameObject);
          }
        }
    }
}
