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
    public Button[] weaponButtons; // Danh sách các nút mua/sử dụng vũ khí
    public int[] weaponPrices; // Giá của các vũ khí tương ứng
    private bool[] weaponPurchased; // Trạng thái đã mua của từng vũ khí
    private int activeWeaponIndex = -1; // Chỉ số vũ khí đang được sử dụng (-1 là chưa sử dụng vũ khí nào)
    public Transform weaponHolder; // Vị trí giữ vũ khí trên Player
    //public GameObject[] weaponPrefabs; // Danh sách Prefab của các vũ khí
    public Sprite[] weaponSprites; // Danh sách Sprite của các vũ khí
    private SpriteRenderer currentWeaponRenderer; // SpriteRenderer hiện tại của vũ khí


    void Start()
    {
        currentWeaponRenderer = weaponHolder.GetComponent<SpriteRenderer>();
        if (currentWeaponRenderer == null)
        {
            Debug.LogError("Không tìm thấy SpriteRenderer trong weaponHolder!");
        }
        shopUI.SetActive(false);

        // Khởi tạo mảng trạng thái vũ khí
        weaponPurchased = new bool[weaponButtons.Length];

        // Tải trạng thái đã lưu từ PlayerPrefs
        LoadWeaponStatus();

        // Thiết lập sự kiện bấm nút mua/sử dụng vũ khí
        for (int i = 0; i < weaponButtons.Length; i++)
        {
            int index = i; // Tạo bản sao để tránh lỗi delegate
            weaponButtons[i].onClick.AddListener(() => HandleWeaponButton(index));
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

    void HandleWeaponButton(int weaponIndex)
    {
        if (!weaponPurchased[weaponIndex])
        {
            BuyWeapon(weaponIndex);
        }
        else
        {
            UseWeapon(weaponIndex);
        }
    }

    void BuyWeapon(int weaponIndex)
    {
        // Kiểm tra đủ vàng từ Staft
        if (playerStats.Coin >= weaponPrices[weaponIndex])
        {
            playerStats.Coin -= weaponPrices[weaponIndex]; // Giảm vàng của người chơi
            weaponPurchased[weaponIndex] = true; // Đánh dấu vũ khí đã được mua

            // Đặt nút thành trạng thái "Sử dụng"
            UpdateButtonToUse(weaponIndex);

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

    void UseWeapon(int weaponIndex)
    {
        if (activeWeaponIndex == weaponIndex)
        {
            Debug.Log("Vũ khí này đã được sử dụng!");
            return;
        }

        // Cập nhật trạng thái nút vũ khí cũ (nếu có)
        if (activeWeaponIndex != -1)
        {
            UpdateButtonToUse(activeWeaponIndex);
        }

        // Đặt vũ khí mới làm vũ khí đang sử dụng
        activeWeaponIndex = weaponIndex;
        // Thay đổi Sprite vũ khí
        if (currentWeaponRenderer != null && weaponSprites.Length > weaponIndex)
        {
            currentWeaponRenderer.sprite = weaponSprites[weaponIndex];
        }
        weaponButtons[weaponIndex].GetComponentInChildren<TextMeshProUGUI>().text = "Dang su dung";
     

        // Lưu trạng thái vũ khí đang sử dụng vào PlayerPrefs
        PlayerPrefs.SetInt("ActiveWeapon", weaponIndex);
        PlayerPrefs.Save();
        Debug.Log("Đang sử dụng vũ khí: " + weaponIndex);

    }

    void LoadWeaponStatus()
    {
        for (int i = 0; i < weaponButtons.Length; i++)
        {
            // Kiểm tra xem vũ khí đã được mua chưa bằng cách sử dụng PlayerPrefs
            if (PlayerPrefs.GetInt("Weapon_" + i, 0) == 1)
            {
                weaponPurchased[i] = true;
                UpdateButtonToUse(i);
            }
        }

        // Tải trạng thái vũ khí đang sử dụng
        activeWeaponIndex = PlayerPrefs.GetInt("ActiveWeapon", -1);
        if (activeWeaponIndex != -1)
        {
            weaponButtons[activeWeaponIndex].GetComponentInChildren<TextMeshProUGUI>().text = "Dang su dung";
        }
    }

    void UpdateButtonToUse(int weaponIndex)
    {
        weaponButtons[weaponIndex].interactable = true;
        weaponButtons[weaponIndex].GetComponentInChildren<TextMeshProUGUI>().text = "Su dung";
    }

    void UpdatePlayerGoldUI()
    {
        playerGoldText.text = "Vàng: " + playerStats.Coin;
    }
}
