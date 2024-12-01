using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class GobinAI : MonoBehaviour
{
    public int maxHealth = 100;    // Máu tối đa
    public int currentHealth;      // Máu hiện tạ
    public Transform player; // Đối tượng Player mà Boss sẽ đuổi theo
    public float moveSpeed = 3f; // Tốc độ di chuyển của Boss
    public float stopDistance = 1.5f; // Khoảng cách để Boss dừng di chuyển
    public float attackDistance =  2f; // Khoảng cách để Boss bắt đầu tấn công
    private int normalDamage = 10; // Sát thương bình thường
    private int enragedDamage = 20; // Sát thương khi cuồng nộ
    private bool isEnraged = false; // Trạng thái cuồng nộ
    private float enragedSpeedMultiplier = 1.5f; // Tăng tốc độ khi cuồng nộ

    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool isAttacking = false;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask PLayer;
    public static GobinAI Intance;

    void Start()
    {
        currentHealth = maxHealth;  // Khởi tạo máu
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // Lấy Animator từ Boss
    }
   
    void Update()
    {
       
        if (player == null) return;

        // Tính toán khoảng cách tới Player
        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        // Nếu khoảng cách trong phạm vi tấn công, dừng di chuyển và tấn công
        if (distance <= attackDistance)
        {
            movement = Vector2.zero;
            if (!isAttacking) StartAttack();
        }
        else if (distance > stopDistance) // Nếu xa hơn khoảng cách dừng, di chuyển
        {
            direction.Normalize();
            movement = direction;
            StopAttack();
        }
        else
        {
            movement = Vector2.zero; // Dừng khi gần nhưng không tấn công
        }

        // Lật mặt Boss theo hướng Player
        FlipSprite(direction.x);
    }

    void FixedUpdate()
    {
        // Di chuyển Boss nếu không tấn công
        if (!isAttacking)
        {
            MoveBoss(movement);
        }
    }

    void MoveBoss(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.fixedDeltaTime));
        animator.SetBool("isMoving", direction != Vector2.zero); // Chuyển đổi animation di chuyển
    }

    void FlipSprite(float directionX)
    {
        if (directionX > 0)
        {
            spriteRenderer.flipX = false; // Hướng phải
        }
        else if (directionX < 0)
        {
            spriteRenderer.flipX = true; // Hướng trái
        }
    }

    void StartAttack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack"); // Kích hoạt animation tấn công
    }

    void StopAttack()
    {
        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Giảm máu
        Debug.Log($"Boss took damage: {damage}, Current Health: {currentHealth}");

        // Nếu máu về 0, Boss chết
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Gobin died.");
        // Xử lý khi Boss chết
    }
   /* public void AttackPlayer()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, PLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            
            
        }
    }*/
  /*  private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth1.Intance.TakeHeath(10);
        }
    }*/


}
