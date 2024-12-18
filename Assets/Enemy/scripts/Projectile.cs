using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject explosionEffectPrefab; // Prefab hiệu ứng vụ nổ
    public int damage = 10; // Sát thương gây ra

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu viên đạn trúng Player
        if (collision.CompareTag("Player"))
        {
            // Gây sát thương cho Player
            HeathPlayer playerHealth = collision.GetComponent<HeathPlayer>();
            if (playerHealth != null)
            {
                playerHealth.TakeHeath(damage);
            }

            // Sinh ra hiệu ứng vụ nổ tại vị trí viên đạn
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

            // Hủy viên đạn sau khi va chạm
            Destroy(gameObject);
        }
      
    }
}
