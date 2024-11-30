using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        // Kiểm tra xem player có phải là null không
        if (player == null)
        {
            Debug.Log("Player not found! Please ensure there is a GameObject with the 'Player' tag in the scene.");
            return; // Dừng thực hiện nếu không tìm thấy player
        }

        // Kiểm tra xem rb có phải là null không
        if (rb == null)
        {
            Debug.Log("Rigidbody2D component not found on this GameObject.");
            return; // Dừng thực hiện nếu không tìm thấy Rigidbody2D
        }
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            HeathPlayer.Intance.TakeHeath(20);
        }
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
