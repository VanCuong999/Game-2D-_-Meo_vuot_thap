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
    private Animator animator; // Biến điều khiển animation
    private bool isAttacking = false; // Trạng thái đang tấn công

    private bool isFrozen = false; // Trạng thái đóng băng
    private Rigidbody2D rb; // Rigidbody2D của enemy (Thêm vào để dừng chuyển động khi đóng băng)
    public GameObject freezeEffectPrefab; // Prefab hiệu ứng băng
    private GameObject currentFreezeEffect; // Hiệu ứng băng đang được sử dụng

    void Start()
    {
        // Tìm player khi script được khởi tạo
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>(); // Lấy Animator từ enemy
        rb = GetComponent<Rigidbody2D>(); // Lấy Rigidbody2D từ enemy
    }

    void Update()
    {
        if (player == null || isFrozen) return; // Nếu enemy bị đóng băng hoặc không tìm thấy player

        // Nếu enemy đã chết, không làm gì nữa
        if (health <= 0) return;

        // Tăng thời gian đếm ngược cho việc bắn laser
        fireTimer += Time.deltaTime;

        // Tính khoảng cách giữa enemy và player
        float distance = Vector2.Distance(transform.position, player.transform.position);

        // Kiểm tra nếu player trong phạm vi tấn công
        if (distance <= detectionRange)
        {
            isPlayerInRange = true; // Player trong vùng xanh
            RotateTowardsPlayer();  // Quay về phía player

            if (fireTimer >= fireDelay && !isAttacking)
            {
                fireTimer = 0f;          // Reset thời gian chờ
                isAttacking = true;      // Bắt đầu tấn công
                if (animator != null)
                {
                    animator.SetTrigger("tancong"); // Kích hoạt animation tấn công
                    ShootLaser();  // Gọi hàm bắn laser
                }
            }
        }
        else
        {
            isPlayerInRange = false; // Player ra ngoài vùng xanh
            StopAttack();           // Dừng tấn công
        }
    }

    void ShootLaser()
    {
        if (!isPlayerInRange || health <= 0) return; // Không bắn nếu player ra khỏi vùng hoặc enemy đã chết

        // Tạo tia laser tại vị trí firePoint
        GameObject laser = Instantiate(laserPrefab, firePoint.position, Quaternion.identity);
        Vector2 direction = (player.transform.position - firePoint.position).normalized;

        Rigidbody2D laserRb = laser.GetComponent<Rigidbody2D>();
        if (laserRb != null)
        {
            laserRb.velocity = direction * 10f; // Tốc độ tia laser
        }

        Debug.Log("Enemy đã bắn laser vào player!");
    }

    void StopAttack()
    {
        if (isAttacking)
        {
            isAttacking = false; // Reset trạng thái tấn công
            if (animator != null)
            {
                animator.ResetTrigger("tancong"); // Dừng animation tấn công
                animator.SetTrigger("dungim");    // Kích hoạt trạng thái đứng im
            }
        }
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

        isFrozen = true; // Đóng băng enemy
        Debug.Log("Enemy bắt đầu bị đóng băng!");

        // Dừng chuyển động
        if (rb != null) rb.velocity = Vector2.zero;

        // Tắt animation
        if (animator != null) animator.enabled = false;

        // Tạo hiệu ứng băng dưới chân enemy
        if (freezeEffectPrefab != null)
        {
            currentFreezeEffect = Instantiate(freezeEffectPrefab, transform.position, Quaternion.identity);
            currentFreezeEffect.transform.SetParent(transform); // Gắn hiệu ứng vào enemy
        }

        StartCoroutine(ThawOut(duration)); // Đặt thời gian chờ để đóng băng
    }

    private IEnumerator ThawOut(float duration)
    {
        yield return new WaitForSeconds(duration); // Chờ hết thời gian đóng băng
        isFrozen = false; // Khôi phục trạng thái bình thường

        // Bật lại animation
        if (animator != null) animator.enabled = true;

        // Xóa hiệu ứng băng
        if (currentFreezeEffect != null)
        {
            Destroy(currentFreezeEffect);
            currentFreezeEffect = null;
        }
    }

}
