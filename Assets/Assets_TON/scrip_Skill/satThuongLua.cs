using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class satThuongLua : MonoBehaviour
{
    public int damage = 60;             // Sát th??ng c?a qu? c?u l?a
    public float speed = 10f;           // T?c ?? di chuy?n c?a qu? c?u l?a
    public float lifetime = 5f;         // Th?i gian s?ng t?i ?a c?a qu? c?u l?a
    private bool hasHit = false;        // Ki?m tra n?u qu? c?u ?ã gây sát th??ng
    private Rigidbody2D rb;             // Thêm Rigidbody2D ?? s? d?ng v?t lý di chuy?n

    void Start()
    {
        // T? h?y qu? c?u sau th?i gian lifetime n?u không va ch?m
        Destroy(gameObject, lifetime);

        // L?y Rigidbody2D n?u có
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Di chuy?n qu? c?u l?a theo h??ng c?a ??i t??ng (trái/ph?i)
            rb.velocity = transform.right * speed;  // Di chuy?n theo h??ng right v?i t?c ?? ?ã ??nh
        }
    }

    void Update()
    {
        // N?u qu? c?u ?ã gây sát th??ng r?i, không làm gì thêm
        if (hasHit) return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ki?m tra n?u ??i t??ng va ch?m là enemy và ch?a gây sát th??ng
        if (!hasHit && collision.CompareTag("Enemy"))
        {
            // Gây sát th??ng cho enemy
            collision.GetComponent<enemy3tancong>()?.TakeDamage(damage);

            // ?ánh d?u là ?ã gây sát th??ng, không gây sát th??ng n?a
            hasHit = true;

            // H?y qu? c?u l?a sau khi gây sát th??ng cho quái
            Destroy(gameObject);
        }

        // Ki?m tra n?u ??i t??ng va ch?m là EnemyGolem và ch?a gây sát th??ng
        if (!hasHit && collision.CompareTag("EnemyGolem"))
        {
            // Gây sát th??ng cho enemy
            collision.GetComponent<GolemEnemy>()?.TakeDangage(damage);

            // ?ánh d?u là ?ã gây sát th??ng, không gây sát th??ng n?a
            hasHit = true;

            // H?y qu? c?u l?a sau khi gây sát th??ng cho quái
            Destroy(gameObject);
        }

        // N?u c?u l?a va ch?m v?i b?t k? v?t c?n nào khác (Obstacle)
        if (!hasHit && collision.CompareTag("Obstacle"))
        {
            // H?y qu? c?u l?a khi va ch?m v?i v?t c?n
            Destroy(gameObject);
        }
    }

}
