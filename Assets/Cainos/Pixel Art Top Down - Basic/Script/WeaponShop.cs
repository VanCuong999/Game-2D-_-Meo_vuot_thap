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
    public GameObject[] weaponPrefabs; // Danh sách Prefab của các vũ khí
    private GameObject currentWeaponInstance; // Tham chiếu tới vũ khí hiện tại
    private GameObject currentWeapon; // Vũ khí đang sử dụng


    void Start()
    {
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
        // Kiểm tra nếu vũ khí đang được chọn đã được sử dụng
        if (activeWeaponIndex == weaponIndex)
        {
            Debug.Log("Vũ khí này đã được sử dụng!");
            return;
        }

        // Hủy đối tượng vũ khí hiện tại (nếu có)
        if (currentWeapon != null)
        {
            Destroy(currentWeapon); // Xóa vũ khí cũ
        }

        // Gắn vũ khí mới vào weaponHolder
        if (weaponPrefabs.Length > weaponIndex && weaponPrefabs[weaponIndex] != null)
        {
            currentWeapon = Instantiate(weaponPrefabs[weaponIndex], weaponHolder.position, Quaternion.identity, weaponHolder);
        }

        // Cập nhật chỉ số vũ khí đang sử dụng
        activeWeaponIndex = weaponIndex;

        // Cập nhật giao diện (UI) cho nút của vũ khí
        for (int i = 0; i < weaponButtons.Length; i++)
        {
            if (i == weaponIndex)
            {
                weaponButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "Đang sử dụng";
            }
            else
            {
                UpdateButtonToUse(i);
            }
        }

        // Lưu trạng thái vào PlayerPrefs
        PlayerPrefs.SetInt("ActiveWeapon", weaponIndex);
        PlayerPrefs.Save();

        Debug.Log("Đang sử dụng vũ khí: " + weaponIndex);
    }




    void LoadWeaponStatus()
    {
        // Tải trạng thái vũ khí đã mua
        for (int i = 0; i < weaponButtons.Length; i++)
        {
            if (PlayerPrefs.GetInt("Weapon_" + i, 0) == 1)
            {
                weaponPurchased[i] = true;
                UpdateButtonToUse(i);
            }
        }

        // Tải vũ khí đang sử dụng
        activeWeaponIndex = PlayerPrefs.GetInt("ActiveWeapon", -1);
        if (activeWeaponIndex != -1 && activeWeaponIndex < weaponPrefabs.Length)
        {
            currentWeapon = Instantiate(weaponPrefabs[activeWeaponIndex], weaponHolder.position, Quaternion.identity, weaponHolder);

            // Cập nhật trạng thái nút UI
            weaponButtons[activeWeaponIndex].GetComponentInChildren<TextMeshProUGUI>().text = "Đang sử dụng";
        }
    }



    void UpdateButtonToUse(int weaponIndex)
    {
        weaponButtons[weaponIndex].interactable = true;
        weaponButtons[weaponIndex].GetComponentInChildren<TextMeshProUGUI>().text = "Sử dụng";
    }



    void UpdatePlayerGoldUI()
    {
        playerGoldText.text = "Vàng: " + playerStats.Coin;
    }

    void OnDestroy()
    {
        // Xóa vũ khí hiện tại (nếu có) khi thoát ra
        if (currentWeaponInstance != null)
        {
            Destroy(currentWeaponInstance);
        }
    }
}
