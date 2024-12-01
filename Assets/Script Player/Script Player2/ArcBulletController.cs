using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArcBulletController : MonoBehaviour
{
    public GameObject bloodParticlePrefab;
    public float speed = 5f; // Tốc độ viên đạn
    public float arcHeight = 2f; // Độ cao của cung
    public float distance = 5f; // Khoảng cách bay

    private Vector2 startPosition;
    private Vector2 targetPosition;
    private float startTime;

    public void Initialize(Vector2 direction)
    {
        startPosition = transform.position;
        targetPosition = startPosition + direction.normalized * distance;
        startTime = Time.time;
    }

    void Update()
    {
        float elapsed = (Time.time - startTime) / (distance / speed);
        if (elapsed >= 1f)
        {
            Destroy(gameObject); // Xóa viên đạn khi đã đi qua khoảng cách
            return;
        }

        // Tính toán vị trí theo hình vòng cung
        float x = Mathf.Lerp(startPosition.x, targetPosition.x, elapsed);
        float y = startPosition.y + Mathf.Sin(elapsed * Mathf.PI) * arcHeight;

        transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Đã chạm vào " + other.name);
            GameObject Blood = Instantiate(bloodParticlePrefab, other.transform.position, Quaternion.identity);
            Destroy(Blood, 1f);
            EnemyShoot.Instance.TakeDamge(50);
            if (EnemyShoot.Instance.HeathEnemy <= 0)
            {
                Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
