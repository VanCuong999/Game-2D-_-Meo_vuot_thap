using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NewBehaviourScript : MonoBehaviour
{
    public int playerGold = 100; // Số vàng hiện tại của người chơi
    public int goldDrop = 10; // Số vàng rơi ra từ quái
    public int upgradeCost = 50; // Chi phí nâng cấp ban đầu
    public int goldIncreasePerUpgrade = 5; // Mức tăng vàng rơi mỗi lần nâng cấp

    public TextMeshProUGUI playerGoldText; // UI hiển thị số vàng của người chơi
    public TextMeshProUGUI upgradeCostText; // UI hiển thị chi phí nâng cấp
    public TextMeshProUGUI goldDropText; // UI hiển thị số vàng rơi từ quái
    public Button upgradeButton; // Nút nâng cấp
    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Hàm nâng cấp số vàng rơi
    public void UpgradeGoldDrop()
    {
        if (playerGold >= upgradeCost)
        {
            playerGold -= upgradeCost; // Trừ vàng nâng cấp
            goldDrop += goldIncreasePerUpgrade; // Tăng số vàng rơi
            upgradeCost = Mathf.CeilToInt(upgradeCost * 1.5f); // Tăng chi phí nâng cấp lần tiếp theo

            UpdateUI();
            ShowGoldIncreaseEffect(); // Gọi hàm hiển thị hiệu ứng
        }
        else
        {
            Debug.Log("Not enough gold to upgrade!");
        }
    }

    // Hàm cập nhật giao diện UI
    private void UpdateUI()
    {
        playerGoldText.text = $"{playerGold}";
        upgradeCostText.text = $"{upgradeCost}";
        goldDropText.text = $"{goldDrop}";
        upgradeButton.interactable = playerGold >= upgradeCost; // Chỉ bật nút khi đủ vàng
    }

    // Hiển thị số vàng được tăng
    private void ShowGoldIncreaseEffect()
    {
        Debug.Log($"+{goldIncreasePerUpgrade} gold per monster after upgrade!");
    }

    // Hàm cộng vàng khi quái bị tiêu diệt
    public void AddGoldFromMonster()
    {
        playerGold += goldDrop;
        UpdateUI();
    }
}
