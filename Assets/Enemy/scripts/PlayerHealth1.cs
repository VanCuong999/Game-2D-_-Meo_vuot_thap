using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth1 : MonoBehaviour
{
    public int currentHealth = 100;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died.");
        // Thêm logic chết (ví dụ: hiển thị màn hình thua)
    }
  /*  private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("boss"))
        {
            // Lấy component PlayerHealth1 từ đối tượng Player
            Gobinmau playerHealth = collision.gameObject.GetComponent<Gobinmau>();

            // Kiểm tra nếu Player có script PlayerHealth1
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10); // Trừ 10 máu mỗi lần va chạm
                Debug.Log("gobin dame");
            }
        }
    }*/
}
