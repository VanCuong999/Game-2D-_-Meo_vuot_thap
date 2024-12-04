using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float health = 5f;
    public Transform player;
    public GameObject projectilePrefab;  // Prefab cho viên đạn bắn ra
    public float shootingInterval = 3f;  // Khoảng thời gian bắn lại (s)
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;

    private float lastShotTime;
    private bool isAttacking = false;
    public Transform shootPoint;  // Vị trí bắn chưởng
    public float projectileSpeed = 5f;  // Tốc độ viên đạn
    public float attackRange = 5;  // Tầm bắn chưởng
    private Animator animator;

    public GameObject explosionEffectPrefab;  // Prefab cho hiệu ứng nổ
    void Start()
    {
        animator = GetComponent<Animator>();
        lastShotTime = Time.time;
        spriteRenderer = GetComponent<SpriteRenderer>(); // Lấy SpriteRenderer của Enemy
    }

    void Update()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;  // Tính khoảng cách giữa Enemy và Player

        // Quay mặt Enemy về hướng người chơi
        FlipSprite(direction.x);

        // Kiểm tra tầm bắn, chỉ bắn nếu Player trong phạm vi tấn công
        if (distance <= attackRange)
        {
            // Nếu Player trong phạm vi tầm bắn và đủ thời gian để bắn lại
            if (Time.time - lastShotTime >= shootingInterval)
            {
                ShootProjectile();  // Bắn viên đạn từ shootPoint
                lastShotTime = Time.time;  // Cập nhật thời gian bắn lại
                Ban();
            }
        }
        else
        {
            // Nếu Player ra ngoài tầm bắn, ngừng tấn công
            StopAttack();
        }

        // Di chuyển Enemy về hướng người chơi nếu không tấn công
        direction.Normalize();
        movement = direction;

        // Ngừng tấn công khi Player ra ngoài tầm bắn
        if (distance > attackRange)
        {
            StopAttack();
        }
    }
    public void Ban()
    {
        animator.SetTrigger("Attack");
    }
    void FixedUpdate()
    {
        // Di chuyển Enemy nếu không tấn công
        if (!isAttacking)
        {
            MoveEnemy(movement);
        }
    }

    void MoveEnemy(Vector2 direction)
    {
        transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);
    }

    // Quay mặt Enemy về hướng người chơi
    void FlipSprite(float directionX)
    {
        if (directionX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (directionX < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void StartAttack()
    {
        isAttacking = true;
        // Có thể thêm animation tấn công ở đây (nếu cần)
    }

    void StopAttack()
    {
        isAttacking = false;
        // Không gọi ShootProjectile nếu không tấn công
    }

    void ShootProjectile()
    {
        // Tạo viên đạn (chưởng) bắn ra từ Enemy
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Tính hướng từ Enemy đến Player
        Vector2 direction = (player.position - shootPoint.position).normalized; // Lấy hướng từ Enemy đến Player

        // Đặt tốc độ cho viên đạn theo hướng đã tính toán
        rb.velocity = direction * projectileSpeed;

        // Quay viên đạn theo hướng mà nó di chuyển
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));  // Quay viên đạn theo hướng
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
