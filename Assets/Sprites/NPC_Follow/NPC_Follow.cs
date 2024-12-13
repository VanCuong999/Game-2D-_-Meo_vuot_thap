using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Follow : MonoBehaviour
{
    public Transform player; // Tham chiếu đến player
    public Transform itemVictory; // Tham chiếu đến ItemVictory
    private Animator anim;
    public float followDistance = 2.0f; // Khoảng cách theo dõi
    public float speed = 3.0f; // Tốc độ di chuyển
    private bool isFollowing = false;

    public float fasingDir = 1;
    private bool FashingRight = true;

    public float itemDistance = 3.0f; // Khoảng cách tối đa để NPC chạy đến ItemVictory

    void Start()
    {
        InvokeRepeating("ToggleFollow", 0f, 3f);
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isFollowing)
        {
            FollowPlayer();
        }

        // Kiểm tra khoảng cách đến ItemVictory
        if (Vector3.Distance(transform.position, itemVictory.position) < itemDistance)
        {
            MoveToVictoryItem();
        }
    }

    void ToggleFollow()
    {
        isFollowing = !isFollowing; // Chuyển đổi trạng thái theo dõi
        anim.SetBool("run", false);
    }

    void FollowPlayer()
    {
        // Tính toán vị trí mới
        Vector3 targetPosition = player.position - player.forward * followDistance;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        FacePlayer();
        anim.SetBool("run", true);
    }

    private void MoveToVictoryItem()
    {
        // Di chuyển đến ItemVictory
        transform.position = Vector3.MoveTowards(transform.position, itemVictory.position, speed * Time.deltaTime);
        anim.SetBool("run", true);
        
        // Kiểm tra nếu đã chạm vào ItemVictory
        if (Vector3.Distance(transform.position, itemVictory.position) < 0.1f)
        {
            // Thực hiện hành động khi chạm vào ItemVictory, ví dụ: thu thập item
            Debug.Log("Chạm vào ItemVictory!");
            // Có thể thêm mã để xử lý thu thập item ở đây
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, followDistance);
    }

    private void FacePlayer()
    {
        // Xác định hướng mà kẻ địch nên quay đầu
        if (player.position.x > transform.position.x && !FashingRight)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && FashingRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        fasingDir = fasingDir * -1;
        FashingRight = !FashingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipControler(float x)
    {
        if (x > 0 && !FashingRight)
            Flip();
        else if (x < 0 && FashingRight)
            Flip();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("ItemVictorry2"))
        {
            UIManager.Intance.ActiveVictory();
        }
    }
}
