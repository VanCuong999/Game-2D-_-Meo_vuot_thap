using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xoayTrap : MonoBehaviour
{
    public float Damageplayer = 10f; // Độ mạnh của lực đánh bật

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            HeathPlayer.Intance.TakeHeath(Damageplayer);
        }
        if (other.CompareTag("NPC"))
        {
            HeathNPC_Follow.Instance.TakeDamage(10);
        }
    }
}
