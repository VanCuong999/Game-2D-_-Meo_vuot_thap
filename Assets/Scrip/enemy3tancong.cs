using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy3tancong : MonoBehaviour
{
    public static enemy3tancong Intance;
    public GameObject laserPrefab; // Prefab của tia laser
    public Transform firePoint; // Vị trí xuất phát của tia laser
    public float fireDelay = 1f; // Thời gian chờ giữa các lần bắn
    public float detectionRange = 5f; // Bán kính vùng xanh
    public float health = 100f; // Máu của enemy (100)

    private GameObject player; // Lưu trữ player
    private bool isPlayerInRange = false; // Kiểm tra xem player có trong vùng xanh hay không
    private float fireTimer = 0f; // Đếm ngược thời gian bắn
    private bool facingRight = true; // Biến theo dõi hướng quay của enemy
    private Animator animator; // Biến điều khiển animations
    private bool isFrozen = false; // Trạng thái đóng băng
    private Rigidbody2D rb; // Lưu trữ Rigidbody2D của enemy
    private MonoBehaviour attackController; // Điều khiển logic tấn công
    void Start()
    {
        // Tìm player khi script được khởi tạo
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>(); // Lấy Animator từ enemy
        rb = GetComponent<Rigidbody2D>();
        attackController = this; // Trường hợp tạm thời dùng chính script này làm logic tấn công
    }

    void Update()
    {
        if (player == null || isFrozen) return; // Kiểm tra nếu không tìm thấy player

        // Tăng thời gian đếm ngược cho việc bắn laser
        fireTimer += Time.deltaTime;

        // Tính khoảng cách giữa enemy và player
        float distance = Vector2.Distance(transform.position, player.transform.position);
        isPlayerInRange = distance <= detectionRange;
        // Kiểm tra nếu player vào trong phạm vi tấn công (detectionRange)
        if (distance <= detectionRange)
        {
            isPlayerInRange = true; // Player trong vùng xanh, enemy có thể tấn công
        }
        else
        {
            isPlayerInRange = false; // Player ra ngoài vùng xanh, không tấn công
        }


        if (isPlayerInRange)
        {
            // Nếu player trong vùng xanh, enemy sẽ tấn công
            if (fireTimer >= fireDelay)
            {
                ShootLaser();
                fireTimer = 0f; // Reset thời gian chờ bắn
            }

            // Quay enemy về hướng player
            RotateTowardsPlayer();
        }
        else
        {
            // Nếu player ra ngoài vùng xanh, enemy không làm gì
            // Không di chuyển, không tấn công
        }

    }

    void ShootLaser()
    {
        // Kích hoạt animation bắn
        if (animator != null)
        {
            animator.SetTrigger("tancong");
        }
        // Tạo tia laser tại vị trí firePoint và hướng về phía player
        GameObject laser = Instantiate(laserPrefab, firePoint.position, Quaternion.identity);
        Vector2 direction = (player.transform.position - firePoint.position).normalized;

        // Gán vận tốc cho tia laser (nếu dùng Rigidbody2D)
        Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * 10f; // Tốc độ của tia laser
        }

        Debug.Log("Enemy bắn laser về phía Player!");
    }

    void RotateTowardsPlayer()
    {
        // Tính vector hướng từ enemy tới player
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Kiểm tra và điều chỉnh hướng quay (quay trái hoặc phải)
        if (direction.x > 0 && !facingRight)
        {
            Flip(); // Quay sang phải nếu player ở bên phải
        }
        else if (direction.x < 0 && facingRight)
        {
            Flip(); // Quay sang trái nếu player ở bên trái
        }

        // Đảm bảo chỉ quay quanh trục Y
        transform.rotation = Quaternion.Euler(0f, facingRight ? 0f : 180f, 0f);
    }

    // Lật Golem để thay đổi hướng quay
    public void Flip()
    {
        facingRight = !facingRight;
    }

    public void TakeDamage(float damage)
    {
        health -= damage; // Giảm máu khi nhận sát thương
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
            Debug.Log("Enemy đã chết!");

            // Kích hoạt trạng thái chết trong Animator
            if (animator != null)
            {
                animator.SetTrigger("chet"); // Kích hoạt trigger Die
            }

            // Hủy đối tượng sau animation chết
            Destroy(gameObject, 1.5f); // Delay để animation kịp chạy
    }

    private void OnDrawGizmosSelected()
    {
        // Vẽ vùng xanh trong Editor để kiểm tra bán kính
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    public void Freeze(float duration)
    {
        if (isFrozen)
        {
            Debug.Log("Enemy đã bị đóng băng!"); // Kiểm tra trạng thái đóng băng
            return;
        }

        isFrozen = true;
        Debug.Log("Enemy bắt đầu bị đóng băng!");

        if (rb != null) rb.velocity = Vector2.zero; // Dừng chuyển động
        if (animator != null) animator.enabled = false; // Tắt animation
        if (attackController != null) attackController.enabled = false; // Dừng tấn công

        StartCoroutine(ThawOut(duration));
    }

    private IEnumerator ThawOut(float duration)
    {
        yield return new WaitForSeconds(duration); // Chờ hết thời gian đóng băng
        isFrozen = false;

        if (animator != null) animator.enabled = true; // Bật lại animation
        if (attackController != null) attackController.enabled = true; // Bật lại tấn công
    }
}
