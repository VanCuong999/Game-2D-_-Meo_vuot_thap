using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float moveSpeed = 2f; // Tốc độ di chuyển của Slime
    public float health = 3f; // Sức khỏe của Slime
    private Vector2 moveDirection;

    // Update is called once per frame
    void Update()
    {
        // Di chuyển ngẫu nhiên
        if (Random.Range(0f, 1f) < 0.01f)
        {
            moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        // Nhận input di chuyển từ người chơi (hoặc AI)
        moveDirection = new Vector2(
            Input.GetAxisRaw("Horizontal"), // Nhận input từ phím mũi tên hoặc WASD
            Input.GetAxisRaw("Vertical")
        ).normalized;

        // Di chuyển Slime
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    // Phương thức nhận sát thương
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // Xử lý khi Slime chết
    void Die()
    {
        // Tạo hiệu ứng chia nhỏ nếu cần
        // Ví dụ: Instantiate các mảnh nhỏ của Slime
        Debug.Log("Slime died!");
        for (int i = 0; i < 3; i++) // Tạo 3 phần nhỏ
        {
            GameObject smallSlime = Instantiate(gameObject, transform.position, Quaternion.identity);
            smallSlime.GetComponent<SlimeController>().health = 1; // Đặt sức khỏe của phần nhỏ
        }

        Destroy(gameObject); // Hủy Slime gốc
    }
}
