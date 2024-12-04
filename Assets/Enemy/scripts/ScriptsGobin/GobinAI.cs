using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class GobinAI : MonoBehaviour
{   
    public Transform player;       
    public float moveSpeed = 2f;  
    public float stopDistance = 1.5f; 
    public float attackDistance = 10f; 
    public Transform attackPoint;     
    public float attackRange = 1f;   
    public LayerMask playerLayer;     


    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    
    private bool isAttacking = false;

    public int stompDamage = 20;        
    public float stompRange = 20f;       
    public float stompCooldown = 20f;   
    public Camera mainCamera;          
    private bool canStomp = true;     

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); 
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (player == null) return;
        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;
        if (distance <= attackDistance)
        {
            movement = Vector2.zero;
            if (!isAttacking) StartAttack();
        }
        else if (distance > stopDistance) 
        {
            direction.Normalize();
            movement = direction;
            StopAttack();
        }
        else
        {
            movement = Vector2.zero; 
        }
        FlipSprite(direction.x);
        if (canStomp && distance <= stompRange)
        {
            StartCoroutine(StompJump());
        }
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
        animator.SetTrigger("Attack"); 
    }

    void StopAttack()
    {
        isAttacking = false;
    }

    IEnumerator StompJump()
    {
        canStomp = false;
        Debug.Log("Gobin is preparing for a Stomp!");
        animator.SetTrigger("Stomp");
        yield return new WaitForSeconds(1f);
        Debug.Log("Gobin executes Stomp!");
        DamagePlayerInRange();
        StartCoroutine(ScreenShake(0.2f, 0.3f));
        yield return new WaitForSeconds(stompCooldown);
        canStomp = true;
    }

    void DamagePlayerInRange()
    {
        Collider2D[] players = Physics2D.OverlapCircleAll(attackPoint.position, stompRange, playerLayer);
        foreach (Collider2D playerCollider in players)
        {
            PlayerHealth playerHealth = playerCollider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(stompDamage);
                Debug.Log($"Player bị trừ {stompDamage} máu.");
            }
            else
            {
                Debug.LogWarning("Không tìm thấy GobinHealth trên Player!");
            }
        }
    }

    IEnumerator ScreenShake(float duration, float magnitude)
    {
        Vector3 originalPosition = mainCamera.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            mainCamera.transform.localPosition = new Vector3(
                originalPosition.x + offsetX,
                originalPosition.y + offsetY,
                originalPosition.z
            );

            elapsed += Time.deltaTime;
            yield return null; 
        }

        mainCamera.transform.localPosition = originalPosition;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(attackPoint.position, stompRange);
        }
    }
    
}
