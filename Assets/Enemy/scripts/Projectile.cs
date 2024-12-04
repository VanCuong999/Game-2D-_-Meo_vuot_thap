using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1f;
    public GameObject explosionEffectPrefab;  // Prefab cho hiệu ứng nổ

    // Khi viên đạn va chạm với một đối tượng
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        Animator explosionAnimator = explosion.GetComponent<Animator>();
        if (explosionAnimator != null)
        {
            explosionAnimator.SetTrigger("Explode"); // Gọi trigger để phát animation nổ
        }

        // Kiểm tra xem viên đạn có va chạm với người chơi không
        if (collision.gameObject.CompareTag("Player"))
        {
            // Xử lý sát thương cho người chơi
            //collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject); // Hủy viên đạn sau khi va chạm
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject); // Hủy viên đạn khi va chạm với tường
        }
    }
}
