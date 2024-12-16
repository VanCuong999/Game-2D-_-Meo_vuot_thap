using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBulletLV1 : MonoBehaviour
{
    public GameObject bulletLv1Prefab;
    public Transform bulletTranform;

    public Image ImageCoolDown;
    public float coolDown = 2.0f; // Thay đổi giá trị này để điều chỉnh thời gian hồi chiêu
    private bool isCoolDown;
    void Start()
    {
        ImageCoolDown.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
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

    public void BulletLv1()
    {
        if (isCoolDown) return;
        isCoolDown = true;

        // Tạo viên đạn
        GameObject bullet = Instantiate(bulletLv1Prefab, bulletTranform.position, bulletTranform.rotation);
        
        // Lấy hướng di chuyển
        Vector3 direction = bulletTranform.right; // Hướng của viên đạn
        bullet.GetComponent<BulletL1>().SetDirection(direction); 
    }
}
