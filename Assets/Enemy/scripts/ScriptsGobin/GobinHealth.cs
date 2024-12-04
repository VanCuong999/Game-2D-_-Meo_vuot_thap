using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GobinHealth : MonoBehaviour
{
    public int maxHealth = 100;  // Máu tối đa của người chơi
    public int currentHealth;   // Máu hiện tại của người chơi
    public Slider healthBar;    // Thanh máu (UI Slider)
    public int baseDamage = 10;     // Sát thương cơ bản Gobin gây ra
    private int currentDamage;      // Sát thương hiện tại (chỉ thay đổi khi cuồng nộ)
    private bool isEnraged = false;  // Cờ trạng thái cuồng nộ

    public GobinAI GobinAI;
    void Start()
    {
        currentHealth = maxHealth;
        currentDamage = baseDamage; // Gán sát thương cơ bản ban đầu
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHealthBar();

        if (currentHealth <= 50 && !isEnraged) // Kích hoạt cuồng nộ
        {
            ActivateRageMode();
        }

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
        Debug.Log("Gobin has died!");
        // Có thể thêm các hành động khác ở đây, ví dụ load lại level:
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void ActivateRageMode()
    {
        isEnraged = true;
        Debug.Log("Gobin has activated Rage Mode!");

        // Tăng gấp đôi máu tối đa
        maxHealth *= 2;

        // Hồi đầy máu
        currentHealth = maxHealth;

        // Tăng gấp đôi sát thương
        baseDamage *= 5;
        currentDamage = baseDamage;

        if (GobinAI != null)
        {
            GobinAI.moveSpeed += 1;
            Debug.Log($"Move Speed increased    to {GobinAI.moveSpeed}");
        }

        UpdateHealthBar();

        Debug.Log($"Rage Mode Activated! Damage increased to {currentDamage}");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<GobinHealth>().TakeDamage(10); // Giảm 10 máu khi va chạm với Player

        }
    }
}
