using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGoblinController : MonoBehaviour
{
    public Rigidbody2D rb;           // Rigidbody2D của boss
    public Animator animator;        // Animator để điều khiển Animation
    public float moveSpeed = 1f;     // Tốc độ di chuyển
    public float jumpForce = 5f;     // Lực nhảy (dash)
    public float actionInterval = 2f; // Thời gian giữa các hành động
    public float jumpCooldown = 3f;  // Thời gian hồi chiêu nhảy
    private Vector2 moveDirection;   // Hướng di chuyển
    private float actionTimer = 0f;  // Đếm thời gian cho hành động
    private float jumpTimer = 0f;    // Đếm thời gian cho nhảy
    private bool isJumping = false;  // Boss đang trong trạng thái nhảy

    void Start()
    {
        GenerateRandomDirection(); // Tạo hướng ngẫu nhiên ban đầu
    }

    void Update()
    {
        // Đếm thời gian
        actionTimer += Time.deltaTime;
        jumpTimer += Time.deltaTime;

        // Thực hiện hành động sau mỗi khoảng thời gian
        if (actionTimer >= actionInterval)
        {
            PerformAction();
            actionTimer = 0f;
        }

        // Di chuyển boss nếu không nhảy
        if (!isJumping)
        {
            rb.velocity = moveDirection * moveSpeed;
        }

        // Kích hoạt Animation di chuyển
        animator.SetBool("isMoving", moveDirection != Vector2.zero);
    }

    void PerformAction()
    {
        // Nếu boss có thể nhảy
        if (jumpTimer >= jumpCooldown)
        {
            Jump();
            jumpTimer = 0f; // Reset thời gian hồi chiêu
        }
        else
        {
            // Thay đổi hướng di chuyển ngẫu nhiên
            GenerateRandomDirection();
        }
    }

    void Jump()
    {
        isJumping = true;
        animator.SetTrigger("isJumping"); // Kích hoạt Animation Jump
        rb.velocity = moveDirection * jumpForce; // Thực hiện nhảy (dash)
        Invoke(nameof(EndJump), 0.5f); // Kết thúc trạng thái nhảy sau 0.5 giây
    }

    void EndJump()
    {
        isJumping = false; // Boss kết thúc trạng thái nhảy
    }

    void GenerateRandomDirection()
    {
        // Tạo hướng di chuyển ngẫu nhiên
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        moveDirection = new Vector2(randomX, randomY).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Nếu va chạm với tường, đổi hướng
        if (collision.gameObject.CompareTag("Wall"))
        {
            GenerateRandomDirection();
        }
    }
}
