using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoom : MonoBehaviour
{
    public Transform player; // Tham chiếu đến người chơi
    public float chaseSpeed = 3f; // Tốc độ chạy của kẻ địch
    public float detectionDistance = 10f; // Khoảng cách phát hiện người chơi
    public float explosionDistance = 1f; // Khoảng cách để nổ
    
    public LayerMask playerLayer;

    private Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        // Tính khoảng cách giữa enemy và player
        float distance = Vector3.Distance(transform.position, player.position);

        // Kiểm tra nếu player ở trong khoảng cách phát hiện
        if (distance <= detectionDistance)
        {
            // Kiểm tra nếu khoảng cách lớn hơn explosionDistance để chạy về phía player
            if (distance > explosionDistance)
            {
                // Sử dụng MoveTowards để di chuyển về phía player
                transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
            }
            else
            {
                
            }
        }
    }

    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionDistance); // Khoảng cách phát hiện
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionDistance); // Khoảng cách nổ
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("no");
        }
    }
    public void EnemyAttack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, explosionDistance,playerLayer);

        foreach (Collider2D playerr in hitPlayer)
        {
            Debug.Log("We hit " + player);
            HeathPlayer.Intance .TakeHeath(10);
            Destroy(gameObject);
        }
    }
}