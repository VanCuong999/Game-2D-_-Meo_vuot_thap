using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float moveSpeed = 5f;  // Tốc độ di chuyển
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Di chuyển Player theo hướng của phím mũi tên
        float moveX = Input.GetAxis("Horizontal");  // Di chuyển theo trục X
        float moveY = Input.GetAxis("Vertical");    // Di chuyển theo trục Y
        movement = new Vector2(moveX, moveY).normalized; // Đảm bảo di chuyển theo vector chuẩn

        // Thực hiện di chuyển
        rb.velocity = movement * moveSpeed;
    }

}
