using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BANG : MonoBehaviour
{
    public float freezeRadius = 5f; // Bán kính vùng đóng băng
    public float freezeDuration = 2f; // Thời gian đóng băng
    public LayerMask enemyLayer; // Lớp của kẻ thù (để phát hiện)

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Nhấn phím F để kích hoạt kỹ năng đóng băng
        {
            FreezeEnemies();
        }
    }

    void FreezeEnemies()
    {
        Debug.Log("Đang kiểm tra vùng đóng băng...");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, freezeRadius, enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Debug.Log("Enemy phát hiện trong vùng đóng băng!");
                enemy3tancong enemyAI = enemy.GetComponent<enemy3tancong>();
                if (enemyAI != null)
                {
                    enemyAI.Freeze(freezeDuration);
                }
            }
        }
    }

    // Hiển thị bán kính ảnh hưởng trong Scene View (dùng cho mục đích debug)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, freezeRadius);
    }

}
