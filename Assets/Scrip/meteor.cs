using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteor : MonoBehaviour
{
    public int damage = 20; // Sát thương mỗi quả meteor

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Kiểm tra nếu bom va chạm với người chơi
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Bom va vào người chơi!");
            HeathPlayer.Intance.TakeHeath(damage);

            // Phá hủy bom sau khi va chạm
            Destroy(gameObject);
        }
        // Kiểm tra nếu bom va chạm với mặt đất (hoặc tag "Ground")
        if (collider.CompareTag("Ground"))
        {
            Debug.Log("Bom đã va chạm với mặt đất!");

            // Thực hiện hành động như nổ bom, tạo mảnh vụn hoặc phá hủy bom
           // Explode();

            // Phá hủy bom sau khi nổ
            Destroy(gameObject);
        }
    }
}
