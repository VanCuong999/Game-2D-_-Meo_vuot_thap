using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showbanner : MonoBehaviour
{
    public GameObject banner1; // Kéo thả Banner1 vào đây
    public GameObject banner2; // Kéo thả Banner2 vào đây
    
    private int coin = 0; 
    private int huyhieu = 0;

    public void ShowRandomBanner()
    {
        // Ẩn cả hai banner
        banner1.SetActive(false);
        banner2.SetActive(false);

        // Chọn ngẫu nhiên một banner để hiển thị
        if (Random.Range(0, 2) == 0)
        {
            banner1.SetActive(true);
        }
        else
        {
            banner2.SetActive(true);
        }
    }

    public void HideBanners()
    {
        // Ẩn cả hai banner
        banner1.SetActive(false);
        banner2.SetActive(false);
    }

    public void btnNhanCoin()
    {
        int cointext_gitf = Random.Range(150 ,300); 
        coin += cointext_gitf;
        Debug.Log("Bạn đã nhận "+ cointext_gitf+" coin! Tổng số coin: " + coin);
        HideBanners();
    }
    public void btnNhanHuyHieu()
    {
        int huyhieu_gitf = Random.Range(150 ,300); 
        huyhieu += huyhieu_gitf;
        Debug.Log("Bạn đã nhận "+ huyhieu_gitf+" huy hieu! Tổng số huy hieu: " + coin);
        HideBanners();
    }
}
