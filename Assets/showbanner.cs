using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class showbanner : MonoBehaviour
{
    public Staft staft;
    public GameObject banner1; // Kéo thả Banner1 vào đây
    public GameObject banner2; // Kéo thả Banner2 vào đây

    public GameObject bannerThanhTuu;

    public TextMeshProUGUI textcoin;
    public TextMeshProUGUI textHuyHieu;
    [Header("Text chức năng Gift")]
    
    public TextMeshProUGUI countdownText; // Text hiển thị thời gian đếm ngược

    private float cointext_gitf;
    private float huyhieu_gitf;

    private float timeBetweenBanners = 10f; // 5 phút tính bằng giây
    private float lastBannerTime;

    private void Start() 
    {
        // Tải thời gian hiển thị banner cuối từ PlayerPrefs
        lastBannerTime = PlayerPrefs.GetFloat("LastbannerTime", Time.time);
        float elapsedTime = Time.time - lastBannerTime;

        // Nếu thời gian đã trôi qua nhiều hơn timeBetweenBanners, reset lastBannerTime
        if (elapsedTime >= timeBetweenBanners)
        {
            lastBannerTime = Time.time; // Reset thời gian nếu đã qua 5 phút
        }

        cointext_gitf = Random.Range(150, 300);
        huyhieu_gitf = Random.Range(150, 300);

        textcoin.text = "+" + cointext_gitf;
        textHuyHieu.text = "+" + huyhieu_gitf;

        StartCoroutine(Countdown());
    }

    public void ShowRandomBanner()
    {
        if (Time.time - lastBannerTime >= timeBetweenBanners) // Kiểm tra xem đã 5 phút chưa
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

            lastBannerTime = Time.time; // Reset thời gian
            PlayerPrefs.SetFloat("LastBannerTime", lastBannerTime); // Lưu thời gian
            StartCoroutine(Countdown()); // Bắt đầu lại đếm ngược
        }
        else
        {
            Debug.Log("Cần đợi thêm " + (timeBetweenBanners - (Time.time - lastBannerTime)) + " giây để hiển thị banner.");
        }
    }

    private IEnumerator Countdown()
    {
        float countdownTime = timeBetweenBanners - (Time.time - lastBannerTime);
        while (countdownTime > 0)
        {
            countdownText.text = "Nhan qua sau: " + FormatTime(countdownTime);
            countdownTime -= Time.deltaTime;
            yield return null; // Chờ cho đến frame tiếp theo
        }
        countdownText.text = "San sang nhan qua!";
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0}:{1:00}", minutes, seconds);
    }

    public void HideBanners()
    {
        // Ẩn cả hai banner
        banner1.SetActive(false);
        banner2.SetActive(false);
        bannerThanhTuu.SetActive(false);
    }

    public void btnNhanCoin()
    {
        staft.Coin += cointext_gitf;
        GameManager.Intance.UpdateManager(staft.NangLuong,staft.Coin,staft.HuyHieu);
        Debug.Log("Bạn đã nhận " + cointext_gitf + " coin! Tổng số coin: " + staft.Coin);
        HideBanners();

        // Reset thời gian sau khi nhận thưởng
        lastBannerTime = Time.time;
        PlayerPrefs.SetFloat("LastBannerTime", lastBannerTime); // Lưu thời gian
        StartCoroutine(Countdown()); // Bắt đầu lại đếm ngược
    }

    public void btnNhanHuyHieu()
    {
        staft.HuyHieu += huyhieu_gitf;
        GameManager.Intance.UpdateManager(staft.NangLuong,staft.Coin,staft.HuyHieu);
        Debug.Log("Bạn đã nhận " + huyhieu_gitf + " huy hiệu! Tổng số huy hiệu: " + staft.HuyHieu);
        HideBanners();

        // Reset thời gian sau khi nhận thưởng
        lastBannerTime = Time.time;
        PlayerPrefs.SetFloat("LastBannerTime", lastBannerTime); // Lưu thời gian
        StartCoroutine(Countdown()); // Bắt đầu lại đếm ngược
    }

    public void ShowThanhTuu()
    {
        bannerThanhTuu.SetActive(true);
    }
}