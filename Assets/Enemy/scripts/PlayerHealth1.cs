using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth1 : MonoBehaviour
{
    public int maxHealth = 100; // Máu tối đa
    public int currentHealth; // Máu hiện tại

    private void Start()
    {
        currentHealth = maxHealth; // Khởi tạo máu
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Giảm máu
        Debug.Log($"Player took damage: {damage}, Current Health: {currentHealth}");

        // Kiểm tra nếu máu về 0
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died.");
        // Xử lý khi Player chết
    }
}
