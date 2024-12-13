using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class satThuongLua : MonoBehaviour
{
    public int damage;             // Sát thương của quả cầu lửa
    public float speed = 5f;       // Tốc độ di chuyển
    public float lifetime = 5f;    // Thời gian sống tối đa của quả cầu lửa

    private bool hasHit = false;   // Kiểm tra nếu quả cầu đã gây sát thương

    void Start()
    {
        // Tự hủy quả cầu sau thời gian lifetime nếu không va chạm
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (hasHit) return; // Nếu quả cầu đã gây sát thương, không làm gì thêm

        // Di chuyển quả cầu lửa
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu đối tượng va chạm là enemy và chưa gây sát thương
        if (!hasHit && collision.CompareTag("Enemy"))
        {
            // Gây sát thương cho enemy
            collision.GetComponent<enemy3tancong>()?.TakeDamage(damage);

            // Đánh dấu là đã gây sát thương, không gây sát thương nữa
            hasHit = true;

            // Hủy quả cầu lửa sau khi gây sát thương cho một quái
            Destroy(gameObject);
        }
        // Kiểm tra nếu đối tượng va chạm là enemy và chưa gây sát thương
        if (!hasHit && collision.CompareTag("EnemyGolem"))
        {
            // Gây sát thương cho enemy
            collision.GetComponent<GolemEnemy>()?.TakeDangage(damage);

            // Đánh dấu là đã gây sát thương, không gây sát thương nữa
            hasHit = true;

            // Hủy quả cầu lửa sau khi gây sát thương cho một quái
            Destroy(gameObject);
        }
    }
}
