using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class skillbang : MonoBehaviour
{
    public TextMeshProUGUI playerGemsText; // Dùng TextMesh Pro
    public Button buyIceballButton;  // Đổi tên nút thành buyIceballButton
    public TextMeshProUGUI messageText;

    private int iceballCost = 2000; // Giá của kỹ năng Cầu Băng (đặt giá mới, ví dụ 3000)
    private bool hasBoughtIceball = false;  // Biến đánh dấu đã mua Cầu Băng

    void Start()
    {
        UpdateUI();  // Cập nhật giao diện
        CheckButtonState();  // Kiểm tra trạng thái nút
    }

    public void BuyIceball()  // Đổi tên hàm từ BuyFireball thành BuyIceball
    {
        int playerGems = GetPlayerGems();  // Lấy số lượng đá tím của người chơi

        if (hasBoughtIceball)  // Nếu người chơi đã mua Cầu Băng
        {
            messageText.text = "Bạn đã sở hữu kỹ năng Cầu Băng!";  // Thông báo đã mua
            return;  // Dừng hàm
        }

        if (playerGems >= iceballCost)  // Kiểm tra nếu đủ đá tím
        {
            playerGems -= iceballCost;  // Trừ đá tím
            hasBoughtIceball = true;  // Đánh dấu đã mua kỹ năng
            SetPlayerGems(playerGems);  // Cập nhật lại số lượng đá tím
            UpdateUI();  // Cập nhật giao diện
            messageText.text = "Bạn đã mua kỹ năng Cầu Băng!";  // Thông báo mua thành công
        }
        else
        {
            messageText.text = "Không đủ đá tím!";  // Thông báo nếu không đủ đá tím
        }

        CheckButtonState();  // Kiểm tra lại trạng thái nút
    }

    private void UpdateUI()  // Cập nhật giao diện
    {
        playerGemsText.text = "Đá tím: " + GetPlayerGems();  // Hiển thị số lượng đá tím
    }

    private void CheckButtonState()  // Kiểm tra trạng thái của nút
    {
        int playerGems = GetPlayerGems();  // Lấy số lượng đá tím
        buyIceballButton.interactable = (playerGems >= iceballCost && !hasBoughtIceball);  // Nút chỉ sáng nếu đủ đá tím và chưa mua
    }

    private int GetPlayerGems()  // Lấy số lượng đá tím
    {
        int gems = 0;
        int.TryParse(playerGemsText.text, out gems);  // Chuyển đổi chuỗi thành số
        return gems;
    }

    private void SetPlayerGems(int gems)  // Cập nhật số lượng đá tím
    {
        playerGemsText.text = gems.ToString();  // Chuyển số đá tím thành chuỗi và hiển thị
    }
}
