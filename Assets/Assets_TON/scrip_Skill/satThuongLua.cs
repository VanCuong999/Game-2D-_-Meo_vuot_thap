using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class satThuongLua : MonoBehaviour
{
    public int damage = 60;             // S�t th??ng c?a qu? c?u l?a
    public float speed = 10f;           // T?c ?? di chuy?n c?a qu? c?u l?a
    public float lifetime = 5f;         // Th?i gian s?ng t?i ?a c?a qu? c?u l?a
    private bool hasHit = false;        // Ki?m tra n?u qu? c?u ?� g�y s�t th??ng
    private Rigidbody2D rb;             // Th�m Rigidbody2D ?? s? d?ng v?t l� di chuy?n

    void Start()
    {
        // T? h?y qu? c?u sau th?i gian lifetime n?u kh�ng va ch?m
        Destroy(gameObject, lifetime);

        // L?y Rigidbody2D n?u c�
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Di chuy?n qu? c?u l?a theo h??ng c?a ??i t??ng (tr�i/ph?i)
            rb.velocity = transform.right * speed;  // Di chuy?n theo h??ng right v?i t?c ?? ?� ??nh
        }
    }

    void Update()
    {
        // N?u qu? c?u ?� g�y s�t th??ng r?i, kh�ng l�m g� th�m
        if (hasHit) return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ki?m tra n?u ??i t??ng va ch?m l� enemy v� ch?a g�y s�t th??ng
        if (!hasHit && collision.CompareTag("Enemy"))
        {
            // G�y s�t th??ng cho enemy
            collision.GetComponent<enemy3tancong>()?.TakeDamage(damage);

            // ?�nh d?u l� ?� g�y s�t th??ng, kh�ng g�y s�t th??ng n?a
            hasHit = true;

            // H?y qu? c?u l?a sau khi g�y s�t th??ng cho qu�i
            Destroy(gameObject);
        }

        // Ki?m tra n?u ??i t??ng va ch?m l� EnemyGolem v� ch?a g�y s�t th??ng
        if (!hasHit && collision.CompareTag("EnemyGolem"))
        {
            // G�y s�t th??ng cho enemy
            collision.GetComponent<GolemEnemy>()?.TakeDangage(damage);

            // ?�nh d?u l� ?� g�y s�t th??ng, kh�ng g�y s�t th??ng n?a
            hasHit = true;

            // H?y qu? c?u l?a sau khi g�y s�t th??ng cho qu�i
            Destroy(gameObject);
        }

        // N?u c?u l?a va ch?m v?i b?t k? v?t c?n n�o kh�c (Obstacle)
        if (!hasHit && collision.CompareTag("Obstacle"))
        {
            // H?y qu? c?u l?a khi va ch?m v?i v?t c?n
            Destroy(gameObject);
        }
    }

}
