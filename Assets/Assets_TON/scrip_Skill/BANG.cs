using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BANG : MonoBehaviour
{
    public float freezeRadius = 5f; // Bán kính vùng đóng băng
    public float freezeDuration = 2f; // Thời gian đóng băng
    public LayerMask enemyLayer; // Lớp của kẻ thù (để phát hiện)

    public Image ImageCoolDown;
    public float coolDown = 2.5f; // Thay đổi giá trị này để điều chỉnh thời gian hồi chiêu
    private bool isCoolDown;

    void Start()
    {
        ImageCoolDown.enabled = true; // Đảm bảo hình ảnh được bật
        ImageCoolDown.fillAmount = 0; // FillAmount ban đầu là 0 (trống)
    }

    void Update()
    {
        // Cập nhật hình ảnh hồi chiêu nếu đang trong trạng thái hồi chiêu
        if (isCoolDown)
        {
            ImageCoolDown.fillAmount -= 1 / coolDown * Time.deltaTime;
            Debug.Log("Fill Amount: " + ImageCoolDown.fillAmount);
            // Đảm bảo Image không bị ẩn

            if (ImageCoolDown.fillAmount <= 0)
            {
                ImageCoolDown.fillAmount = 1;
                isCoolDown = false; // Kết thúc hồi chiêu
            }
        }
    }



    private bool IsEnemyInRange()
    {
        // Lấy tất cả các kẻ thù trong phạm vi
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, freezeRadius, enemyLayer);

        // Nếu có ít nhất một kẻ thù trong phạm vi, trả về true
        return enemies.Length > 0;
    }

    public void FreezeEnemies()
    {
        // Kiểm tra xem có đang trong thời gian hồi chiêu không
        if (isCoolDown) return;

        // Bắt đầu hồi chiêu
        isCoolDown = true;
        ImageCoolDown.fillAmount = 1; // Bắt đầu từ đầy (hiển thị hồi chiêu)

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
