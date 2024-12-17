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

    public GameObject explosionEffectPrefab; // Prefab hi?u ?ng n?

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
        if (!hasHit && collision.CompareTag("Enemy"))
        {
            // Gây sát th??ng cho enemy
            collision.GetComponent<enemy3tancong>()?.TakeDamage(damage);

            // G?i hi?u ?ng n?
            CreateExplosion();

            // ?ánh d?u là ?ã gây sát th??ng
            hasHit = true;

            // H?y qu? c?u l?a sau khi gây sát th??ng
            Destroy(gameObject);
        }
        if (!hasHit && collision.CompareTag("Enemy"))
        {
            // Gây sát th??ng cho enemy
            collision.GetComponent<EnemyMove>()?.TakeDangage(damage);

            // G?i hi?u ?ng n?
            CreateExplosion();

            // ?ánh d?u là ?ã gây sát th??ng
            hasHit = true;

            // H?y qu? c?u l?a sau khi gây sát th??ng
            Destroy(gameObject);
        }

        if (!hasHit && collision.CompareTag("EnemyGolem"))
        {
            // Gây sát th??ng cho enemy
            collision.GetComponent<GolemEnemy>()?.TakeDangage(damage);

            // G?i hi?u ?ng n?
            CreateExplosion();

            // ?ánh d?u là ?ã gây sát th??ng
            hasHit = true;

            // H?y qu? c?u l?a sau khi gây sát th??ng
            Destroy(gameObject);
        }

        if (!hasHit && collision.CompareTag("Obstacle"))
        {
            // G?i hi?u ?ng n?
            CreateExplosion();

            // H?y qu? c?u l?a khi va ch?m v?i v?t c?n
            Destroy(gameObject);
        }
    }

    private void CreateExplosion()
    {
        if (explosionEffectPrefab != null)
        {
            // T?o hi?u ?ng n? t?i v? trí c?a qu? c?u l?a
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }
    }

}
