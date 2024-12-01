using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class PlayerHealth1 : MonoBehaviour
{

    private float currentHealth = 100f;
    public static PlayerHealth1 Intance;
    public float minHeath;
    public float maxHeath;

    private float Heath;

    [SerializeField] private Image heathImage;
    [SerializeField] private TextMeshProUGUI heathText;
    [SerializeField] protected GameObject floatingTextPrefab;
    private void Awake()
    {
        Intance = this;
    }
    private void Start()
    {
        Heath = minHeath;
        UPdateHeath(Heath, maxHeath);
    }
    private void Update()
    {
      
    }

   /* public void TakeHeath(float heath)
    {
        ShowDamage(heath.ToString());
        Heath -= heath;
        if (Heath <= 0)
        {
            Debug.Log("player die");

        }

        UPdateHeath(Heath, maxHeath);
    }*/

    public void HoiMau(float heath)
    {
        ShowDamage(heath.ToString());
        Heath += heath;

        if (Heath > maxHeath)
        {
            Heath = maxHeath;
        }

        UPdateHeath(Heath, maxHeath);
    }
    public void UPLevelHeath()
    {
        minHeath += 30;
        maxHeath += 30;
        UPdateHeath(Heath, maxHeath);
    }

    public void UPdateHeath(float minHeaths, float maxHeaths)
    {
        minHeath = minHeaths;
        maxHeath = maxHeaths;
    }

    public void ShowDamage(string text)
    {
        if (floatingTextPrefab)
        {
           
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Giảm máu
        Debug.Log($"Boss took damage: {damage}, Current Health: {currentHealth}");

        // Nếu máu về 0, Boss chết
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Gobin died.");
        // Xử lý khi Boss chết
    }
}
