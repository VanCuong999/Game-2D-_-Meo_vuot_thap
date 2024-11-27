using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [Header("Player chiêu thức 1")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    public Image ImageCoolDown;
    public float coolDown = 2.0f; // Thay đổi giá trị này để điều chỉnh thời gian hồi chiêu
    private bool isCoolDown;

    void Start()
    {
        ImageCoolDown.fillAmount = 1; // Đặt fillAmount ban đầu là 0
    }

    void Update()
    {
        // Cập nhật thời gian hồi chiêu
        if (isCoolDown)
        {
            ImageCoolDown.fillAmount -= 1 / coolDown * Time.deltaTime; // Giảm fillAmount
            if (ImageCoolDown.fillAmount <= 0) // Khi fillAmount đạt 0
            {
                ImageCoolDown.fillAmount = 1; // Đặt lại fillAmount về 1
                isCoolDown = false; // Kết thúc hồi chiêu
            }
        }
    }

    public void CreateBullets()
    {
        // Kiểm tra xem có đang trong thời gian hồi chiêu không
        if (isCoolDown) return;

        // Bắt đầu hồi chiêu
        isCoolDown = true;

        Character.Intance.KichHoatChieuThuc();

        for (int i = 0; i < 10; i++)
        {
            float angle = (i * 2 * Mathf.PI) / 10;

            // Tạo viên đạn
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            // Tính toán hướng bắn dựa trên góc và hướng của nhân vật
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            Vector2 rotatedDirection = Quaternion.Euler(0, 0, transform.eulerAngles.z) * direction; // Quay hướng theo hướng nhân vật
            
            // Thiết lập tốc độ cho viên đạn
            bullet.GetComponent<Rigidbody2D>().velocity = rotatedDirection * bulletSpeed;

            // Quay viên đạn theo hướng bắn
            float bulletAngle = Mathf.Atan2(rotatedDirection.y, rotatedDirection.x) * Mathf.Rad2Deg; // Tính góc viên đạn
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, bulletAngle)); // Quay viên đạn

            Destroy(bullet.gameObject, 2f);
        }
    }
}