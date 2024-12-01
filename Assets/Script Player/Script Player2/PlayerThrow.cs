using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    public GameObject arcBulletPrefab;
    public Transform firePoint; // Điểm xuất phát viên đạn

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Thay thế bằng input của bạn
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(arcBulletPrefab, firePoint.position, Quaternion.identity);
        ArcBulletController bulletController = bullet.GetComponent<ArcBulletController>();

        // Tính toán hướng bắn
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position);
        // Đảm bảo hướng là 2D
        bulletController.Initialize(direction);
    }
}