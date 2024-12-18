﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : MonoBehaviour
{

    [SerializeField] private LayerMask enemyLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Projectile collided with: " + collision.gameObject.name);

        // Kiểm tra nếu đối tượng thuộc lớp quái
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            Debug.Log("Hit enemy layer: " + collision.gameObject.name);

            // Lấy script Enemy và gọi TakeDamage
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("Enemy script found on: " + collision.gameObject.name);
               // enemy.TakeDamage(10);
            }
            else
            {
                Debug.LogWarning("Enemy script not found on: " + collision.gameObject.name);
            }
            
        }
        if (collision.CompareTag("EnemyGolem"))
        {
            GolemEnemy golemEnemy = collision.GetComponent<GolemEnemy>();
            golemEnemy.TakeDangage(30);
        }
    }
   

}
