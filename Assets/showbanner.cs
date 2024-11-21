using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class showbanner : MonoBehaviour
{
    public GameObject banner1; // Kéo thả Banner1 vào đây
    public GameObject banner2; // Kéo thả Banner2 vào đây

    public TextMeshProUGUI textcoin;
    public TextMeshProUGUI textHuyHieu;
    private int coin = 0;
    private int huyhieu = 0;

    private int cointext_gitf;
    private int huyhieu_gitf;


    private void Start() 
    {
        cointext_gitf = Random.Range(150, 300);
        huyhieu_gitf = Random.Range(150, 300);

        textcoin.text = "+"+ cointext_gitf;
        textHuyHieu.text = "+"+ huyhieu_gitf;
    }
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

        coin += cointext_gitf;
        Debug.Log("Bạn đã nhận " + cointext_gitf + " coin! Tổng số coin: " + coin);
        HideBanners();
    }
    public void btnNhanHuyHieu()
    {

        huyhieu += huyhieu_gitf;
        Debug.Log("Bạn đã nhận " + huyhieu_gitf + " huy hieu! Tổng số huy hieu: " + huyhieu);
        HideBanners();
    }
}
