using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [Header("Combat Settings")]
    public float detectionRadius = 5f;  // Phạm vi phát hiện người chơi
    public float attackRange = 2f;      // Phạm vi tấn công
    public float attackCooldown = 2f;   // Thời gian chờ giữa các đòn tấn công
    public LayerMask playerLayer;       // Lớp đối tượng người chơi
    public Transform attackPoint;       // Vị trí tấn công (thường là vị trí tay hoặc vũ khí của boss)
    private float attackTimer;          // Timer cho việc tấn công

    private bool isPlayerDetected = false;  // Kiểm tra xem có phát hiện người chơi không
    private Transform player;           // Tham chiếu đến người chơi
    private Animator anim;              // Tham chiếu đến Animator
    private Rigidbody2D rb;

    private bool isComboAttack = false;  // Kiểm tra xem boss có đang thực hiện combo không
    private bool canPerformCombo = false; // Kiểm tra xem có thể thực hiện combo sau khi tấn công 1
    private float comboCooldown = 0.5f;  // Thời gian chờ giữa các đòn combo (tùy chỉnh)
    private float comboTimer = 0f;       // Timer cho combo

    private int attack2Count = 0;  // Biến đếm số lần thực hiện Attack2
    private bool isAttacking = false;
    [Header("Health Enemy")]
    [SerializeField] private Image _heathBarFill;  // Health bar của boss
    [SerializeField] private Transform _healthBarTranForm; // Vị trí health bar
    public float HeathEnemy = 100;     // Sức khỏe tối đa của boss
    private float currentHeath;        // Sức khỏe hiện tại của boss
    private Color fullHealthColor = Color.green;   // Màu sắc cho thanh máu đầy
    private Color lowHealthColor = Color.yellow;   // Màu sắc cho thanh máu thấp
    private Color minimumHealthColor = Color.red;  // Màu sắc cho thanh máu nguy kịch

    [Header("Movement Settings")]
    public float moveSpeed = 2f;  // Tốc độ di chuyển của boss

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHeath = HeathEnemy;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;  // Tìm đối tượng có tag "Player"
        anim = GetComponent<Animator>();  // Lấy Animator của boss
    }

    private void Update()
    {
        if (currentHeath <= 0) return; // Nếu boss đã chết, không làm gì

        // Kiểm tra có phát hiện người chơi không
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(player.position, transform.position);
            isPlayerDetected = distanceToPlayer <= detectionRadius; // Kiểm tra người chơi có trong phạm vi phát hiện

            if (isPlayerDetected && distanceToPlayer <= attackRange)
            {
                AttackPlayer();  // Nếu người chơi ở gần đủ, boss sẽ tấn công
            }
            else if (isPlayerDetected)
            {
                // Nếu phát hiện người chơi nhưng chưa trong phạm vi tấn công, tiếp tục di chuyển đến người chơi
                ChasePlayer();
            }
            else
            {
                // Nếu không phát hiện người chơi, dừng lại
                anim.SetBool("isMoving", false);  // Boss không di chuyển
            }
        }

        // Cập nhật timer tấn công
        attackTimer -= Time.deltaTime;

        // Cập nhật animation
       
    }

    

    private void ChasePlayer()
    {
        // Tính khoảng cách giữa boss và người chơi
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);

        // Lật mặt boss về hướng người chơi
        FlipToPlayer();

        // Nếu boss còn xa người chơi (lớn hơn phạm vi tấn công), di chuyển về phía người chơi
        if (distanceToPlayer > attackRange)
        {
            // Tính hướng di chuyển của boss
            Vector2 direction = (player.position - transform.position).normalized;

            // Kiểm tra nếu boss di chuyển quá gần người chơi (dưới 2f), thì dừng lại
            if (distanceToPlayer > attackRange + 0.1f)  // Thêm 0.1f để tránh boss dừng quá sớm
            {
                rb.velocity = direction * moveSpeed;  // Tốc độ di chuyển của boss
                anim.SetBool("isMoving", true);  // Kích hoạt animation di chuyển
            }
            else
            {
                rb.velocity = Vector2.zero;  // Dừng di chuyển khi gần đủ phạm vi tấn công
                anim.SetBool("isMoving", false);  // Dừng animation di chuyển
                AttackPlayer();  // Tấn công người chơi
            }
        }
        else
        {
            // Nếu đã vào phạm vi tấn công, dừng di chuyển và tấn công
            rb.velocity = Vector2.zero;  // Dừng di chuyển
            anim.SetBool("isMoving", false);  // Dừng animation di chuyển
            AttackPlayer();  // Tấn công người chơi
        }
    }

    // Hàm lật mặt boss về hướng người chơi
    private void FlipToPlayer()
    {
        // Kiểm tra hướng của boss so với người chơi
        float directionToPlayer = player.position.x - transform.position.x;

        // Nếu người chơi ở bên phải boss, lật mặt sang phải
        if (directionToPlayer > 0)
        {
            if (transform.localScale.x < 0) // Nếu boss đang nhìn về bên trái, lật lại
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        // Nếu người chơi ở bên trái boss, lật mặt sang trái
        else
        {
            if (transform.localScale.x > 0) // Nếu boss đang nhìn về bên phải, lật lại
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private void AttackPlayer()
    {
        if (attackTimer <= 0)
        {
            if (isAttacking)  // Nếu boss đang tấn công
            {
                // Nếu đã thực hiện đủ Attack2, quay lại Attack1
                if (attack2Count >= 3)  // Ví dụ thực hiện 3 lần Attack2
                {
                    anim.SetTrigger("Attack1");  // Kích hoạt animation Attack1
                    attackTimer = attackCooldown;  // Đặt lại thời gian chờ cho Attack1
                    attack2Count = 0;  // Reset đếm Attack2
                    isAttacking = false;  // Dừng tấn công
                }
                else
                {
                    // Nếu chưa đủ số lần Attack2, tiếp tục thực hiện Attack2
                    anim.SetTrigger("Attack2");  // Kích hoạt animation Attack2
                    attackTimer = attackCooldown;  // Đặt lại thời gian chờ cho Attack2
                    attack2Count++;  // Tăng đếm Attack2
                }
            }
            else
            {
                // Nếu chưa tấn công, thực hiện Attack1
                anim.SetTrigger("Attack1");  // Kích hoạt animation Attack1
                attackTimer = attackCooldown;  // Đặt lại thời gian chờ cho Attack1
                isAttacking = true;  // Bắt đầu tấn công
                attack2Count = 0;  // Reset đếm Attack2
            }
        }
    }

    // Hàm cập nhật thanh máu của boss
    public void UpdateHealth(float health)
    {
        HeathEnemy = health;
        _heathBarFill.fillAmount = currentHeath / HeathEnemy;  // Cập nhật tỷ lệ thanh máu
        UpdateHealthColor();
    }

    // Hàm cập nhật màu sắc thanh máu
    public void UpdateHealthColor()
    {
        float healthPercentage = currentHeath / HeathEnemy;

        if (healthPercentage <= 0.3f) // Dưới 30%
        {
            _heathBarFill.color = minimumHealthColor;
        }
        else if (healthPercentage <= 0.7f) // Dưới 70%
        {
            _heathBarFill.color = lowHealthColor;
        }
        else // Trên 70%
        {
            _heathBarFill.color = fullHealthColor;
        }
    }

    // Hàm nhận sát thương từ người chơi
    public void TakeDamage(float damage)
    {
        currentHeath -= damage;

        if (currentHeath <= 0)
        {
            Die();  // Boss chết
        }

        UpdateHealth(HeathEnemy);  // Cập nhật thanh máu
    }

    private void Die()
    {
        // Khi boss chết, vô hiệu hóa các collider và set trạng thái chết
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null) sprite.enabled = false;

        if (_healthBarTranForm != null)
        {
            _healthBarTranForm.gameObject.SetActive(false);  // Tắt thanh máu
        }

        // Thực hiện hồi sinh hoặc các hành động khác ở đây nếu cần
        // Tạm thời dừng lại
    }

}
