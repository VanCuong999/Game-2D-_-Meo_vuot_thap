using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 200;  // Máu tối đa của người chơi
    public int currentHealth;    // Máu hiện tại của người chơi
    public Slider healthBar;     // Thanh máu (UI Slider)

   

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        // Xử lý khi người chơi chết (Game Over, Respawn, etc.)
        Debug.Log("Player has died!");
        // Có thể thêm các hành động khác ở đây, ví dụ load lại level:
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("boss"))
        {
            // Lấy đối tượng Boss và lấy thông tin về sát thương hiện tại
           
            GetComponent<PlayerHealth>().TakeDamage(10); // Giảm 10 máu khi va chạm với Player
        }
    }
}
