using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 5f; // Tốc độ di chuyển
    private Rigidbody2D rb; // Tham chiếu đến Rigidbody2D
    private Vector2 movement; // Lưu trữ vector di chuyển

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Lấy Rigidbody2D của nhân vật
        rb.gravityScale = 0; // Tắt trọng lực
    }

    void Update()
    {
        // Lấy input từ bàn phím
        movement.x = Input.GetAxisRaw("Horizontal"); // Trục X: A/D hoặc Mũi tên trái/phải
        movement.y = Input.GetAxisRaw("Vertical");   // Trục Y: W/S hoặc Mũi tên lên/xuống
    }

    void FixedUpdate()
    {
        // Di chuyển nhân vật bằng Rigidbody2D
        rb.velocity = movement * moveSpeed;
    }
}
