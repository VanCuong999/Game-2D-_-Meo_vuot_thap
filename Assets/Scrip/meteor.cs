using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteor : MonoBehaviour
{
    public int damage = 20; // Sát thương mỗi quả meteor
    public GameObject explosionEffect; // Prefab hiệu ứng vỡ
    private bool hasExploded = false; // Kiểm tra nếu hiệu ứng đã xảy ra

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Kiểm tra nếu meteor đã nổ trước đó
        if (hasExploded) return;

        // Va chạm với người chơi
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Meteor va vào người chơi!");
            HeathPlayer.Intance.TakeHeath(damage);

            // Gọi hiệu ứng nổ
            Explode();
        }
        // Va chạm với mặt đất
        else if (collider.CompareTag("Ground"))
        {
            Debug.Log("Meteor đã va chạm với mặt đất!");

            // Gọi hiệu ứng nổ
            Explode();
        }
    }

    // Hàm tạo hiệu ứng nổ
    private void Explode()
    {
        if (!hasExploded) // Đảm bảo hiệu ứng chỉ chạy một lần
        {
            hasExploded = true;

            // Tạo hiệu ứng tại vị trí meteor
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            // Phá hủy meteor sau khi nổ
            Destroy(gameObject);
        }
    }
}
