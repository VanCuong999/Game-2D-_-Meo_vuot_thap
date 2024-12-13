using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BANG : MonoBehaviour
{
    public float freezeRadius = 10f; // Bán kính ảnh hưởng
    public float freezeDuration = 2f; // Thời gian đóng băng
    public LayerMask enemyLayer; // Lớp của kẻ thù để phát hiện

    void Update()
    {
        // Kích hoạt kỹ năng bằng phím F
        if (Input.GetKeyDown(KeyCode.F))
        {
            FreezeEnemies();
        }
    }

    void FreezeEnemies()
    {
        // Lấy tất cả các đối tượng trong bán kính ảnh hưởng
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, freezeRadius, enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            // Kiểm tra nếu đối tượng có tag "Enemy"
            if (enemy.CompareTag("Enemy"))
            {
                // Lấy script quản lý hành vi của kẻ thù
                dongBang enemyBehavior = enemy.GetComponent<dongBang>();

                if (enemyBehavior != null)
                {
                    enemyBehavior.Freeze(freezeDuration); // Gọi hàm đóng băng từ script của kẻ thù
                }
            }
        }
    }

    // Vẽ bán kính ảnh hưởng trong Scene View
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan; // Màu của vùng ảnh hưởng
        Gizmos.DrawWireSphere(transform.position, freezeRadius);
    }
}
