using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    public GameObject shopUI; // Giao diện cửa hàng
    public TextMeshProUGUI playerGoldText; // Text hiển thị số vàng của người chơi
    public Staft playerStats; // Tham chiếu đến ScriptableObject Staft
    public Button[] weaponButtons; // Danh sách các nút mua vũ khí
    public int[] weaponPrices; // Giá của các vũ khí tương ứng
    private bool[] weaponPurchased; // Trạng thái đã mua của từng vũ khí

    void Start()
    {
        shopUI.SetActive(false);

        // Khởi tạo mảng trạng thái vũ khí
        weaponPurchased = new bool[weaponButtons.Length];

        // Tải trạng thái đã lưu từ PlayerPrefs
        LoadWeaponStatus();

        // Thiết lập sự kiện bấm nút mua vũ khí
        for (int i = 0; i < weaponButtons.Length; i++)
        {
            int index = i; // Tạo bản sao để tránh lỗi delegate
            weaponButtons[i].onClick.AddListener(() => BuyWeapon(index));
        }

        // Hiển thị vàng ban đầu từ Staft
        UpdatePlayerGoldUI();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopUI.SetActive(true); // Hiển thị giao diện cửa hàng
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopUI.SetActive(false); // Ẩn giao diện cửa hàng
        }
    }

    void BuyWeapon(int weaponIndex)
    {
        // Kiểm tra nếu vũ khí đã được mua
        if (weaponPurchased[weaponIndex])
        {
            Debug.Log("Vũ khí này đã được mua rồi!");
            return;
        }

        // Kiểm tra đủ vàng từ Staft
        if (playerStats.Coin >= weaponPrices[weaponIndex])
        {
            playerStats.Coin -= weaponPrices[weaponIndex]; // Giảm vàng của người chơi
            weaponPurchased[weaponIndex] = true; // Đánh dấu vũ khí đã được mua

            // Vô hiệu hóa nút mua
            weaponButtons[weaponIndex].interactable = false;
            weaponButtons[weaponIndex].GetComponentInChildren<TextMeshProUGUI>().text = "Mua Roi"; // Cập nhật text nút

            // Lưu trạng thái đã mua vào PlayerPrefs
            PlayerPrefs.SetInt("Weapon_" + weaponIndex, 1);
            PlayerPrefs.Save();

            Debug.Log("Đã mua vũ khí: " + weaponIndex);
            UpdatePlayerGoldUI();
        }
        else
        {
            Debug.Log("Không đủ vàng!");
        }
    }

    void LoadWeaponStatus()
    {
        for (int i = 0; i < weaponButtons.Length; i++)
        {
            // Kiểm tra xem vũ khí đã được mua chưa bằng cách sử dụng PlayerPrefs
            if (PlayerPrefs.GetInt("Weapon_" + i, 0) == 1)
            {
                weaponPurchased[i] = true;
                weaponButtons[i].interactable = false;
                weaponButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "Mua Roi"; // Cập nhật text nút
            }
        }
    }

    void UpdatePlayerGoldUI()
    {
        playerGoldText.text = "Vàng: " + playerStats.Coin;
    }

}
