using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float patrolRadius = 5f; // Bán kính tuần tra
    public float speed = 2f; // Tốc độ di chuyển
    public float pauseTime = 2f; // Thời gian dừng giữa các lần di chuyển

    [Header("Combat Settings")]
    public float detectionRadius = 5f; // Bán kính phát hiện người chơi
    public float attackRange = 5f; // Phạm vi tấn công
    public float attackCooldown = 3f; // Thời gian chờ giữa các lần tấn công
    public LayerMask playerLayer; // Lớp người chơi
    private float attackTimer; // Bộ đếm thời gian tấn công
    private bool isPlayerDetected = false; // Kiểm tra xem người chơi có bị phát hiện không

    private Vector2 startPosition; // Vị trí ban đầu
    private Vector2 targetPosition; // Vị trí mục tiêu tuần tra
    private bool isPatrolling = true; // Trạng thái tuần tra
    private bool FashingRight = true;
    private Transform player; // Tham chiếu tới người chơi
    private Animator anim;

    [Header("Fireball Settings")]
    public GameObject fireballPrefab; // Cầu lửa prefab
    public Transform fireballSpawnPoint; // Vị trí sinh ra cầu lửa
    public float fireballSpeed = 5f; // Tốc độ cầu lửa

    private void Start()
    {
        startPosition = transform.position;
        ChooseRandomDirection();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(player.position, transform.position);

            // Kiểm tra nếu người chơi vào trong phạm vi phát hiện
            if (distanceToPlayer <= detectionRadius)
            {
                isPlayerDetected = true;
            }
            else
            {
                isPlayerDetected = false;
            }

            // Nếu người chơi bị phát hiện và khoảng cách <= attackRange thì dừng lại và bắn cầu lửa
            if (isPlayerDetected)
            {
                if (distanceToPlayer <= attackRange)
                {
                    // Dừng lại và tấn công bằng cầu lửa nếu thời gian chờ đủ
                    if (attackTimer <= 0)
                    {
                        AttackPlayerWithFireball();
                        attackTimer = attackCooldown; // Reset bộ đếm thời gian
                    }
                }
                else
                {
                    // Tiến lại gần người chơi nhưng không vượt quá attackRange
                    ChasePlayer(distanceToPlayer);
                }
            }
            else
            {
                Patrol();
            }
        }

        // Giảm bộ đếm thời gian sau mỗi khung hình
        attackTimer -= Time.deltaTime;
    }

    private void Patrol()
    {
        if (isPatrolling)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Kiểm tra nếu đến gần vị trí mục tiêu
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                isPatrolling = false;
                StartCoroutine(PauseBeforeMoving());
            }
        }
    }

    private IEnumerator PauseBeforeMoving()
    {
        yield return new WaitForSeconds(pauseTime);
        ChooseRandomDirection();
        isPatrolling = true;
    }

    private void ChooseRandomDirection()
    {
        // Chọn một trong bốn hướng ngẫu nhiên (trên, dưới, trái, phải)
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        Vector2 randomDirection = directions[Random.Range(0, directions.Length)];

        // Tính toán vị trí mục tiêu mới trong bán kính tuần tra
        targetPosition = startPosition + randomDirection * patrolRadius;

        // Kiểm tra nếu vị trí vượt ra ngoài bán kính tuần tra thì điều chỉnh lại
        if (Vector2.Distance(startPosition, targetPosition) > patrolRadius)
        {
            targetPosition = startPosition + randomDirection * patrolRadius * 0.8f;
        }

        // Lật mặt theo hướng di chuyển
        Flip(randomDirection);
    }

    private void ChasePlayer(float distanceToPlayer)
    {
        if (distanceToPlayer > attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        // Lật mặt theo hướng người chơi
        Vector2 direction = player.position - transform.position;
        Flip(direction);
    }

    private void AttackPlayerWithFireball()
    {
        if (fireballPrefab != null && fireballSpawnPoint != null)
        {
            // Sinh ra cầu lửa tại vị trí spawn
            GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);
            Vector2 direction = (player.position - fireballSpawnPoint.position).normalized;

            // Lật mặt Enemy theo hướng người chơi
            Flip(direction);

            // Tốc độ di chuyển của cầu lửa
            fireball.GetComponent<Rigidbody2D>().velocity = direction * fireballSpeed;
        }
    }

    private void Flip(Vector2 direction)
    {
        // Nếu hướng đi sang phải và không phải là đang nhìn phải
        if (direction.x > 0 && !FashingRight)
        {
            FashingRight = true;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        // Nếu hướng đi sang trái và đang nhìn phải
        else if (direction.x < 0 && FashingRight)
        {
            FashingRight = false;
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }


    private void OnDrawGizmosSelected()
    {
        // Vẽ bán kính tuần tra và phát hiện
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(startPosition, patrolRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
