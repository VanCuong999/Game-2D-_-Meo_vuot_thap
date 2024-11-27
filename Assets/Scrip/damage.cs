using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class damage : MonoBehaviour
{
    public int dama = 10; // Sát thương chiêu thức hiện tại
    public int upgradeCost = 100; // Chi phí nâng cấp sát thương
    public int damageIncrease = 15; // Sát thương tăng thêm mỗi lần nâng cấp
    public int playerGold = 1000; // Số vàng hiện tại của người chơi

    public TextMeshProUGUI playerGoldText; // UI hiển thị số vàng của người chơi
    public TextMeshProUGUI upgradeCostText; // Text để hiển thị chi phí nâng cấp
    public TextMeshProUGUI damageIncreaseText; // Text để hiển thị sát thương được tăng thêm
    public Button upgradeButton; // Button để nâng cấp
    // Start is called before the first frame update
    void Start()
    {
        // Cập nhật giao diện khi bắt đầu
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void UpgradeDamage()
    {
        if (playerGold >= upgradeCost)
        {
            // Trừ vàng và tăng sát thương
            playerGold -= upgradeCost;
            dama += damageIncrease;
            damageIncrease += 5; // Tăng sát thương tăng thêm mỗi lần nâng cấp
            upgradeCost *= 2; // Tăng chi phí nâng cấp cho lần sau

            // Cập nhật giao diện
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        playerGoldText.text = $"{playerGold}";
        upgradeCostText.text = $"{upgradeCost}";
        damageIncreaseText.text = $"{damageIncrease}";
        upgradeButton.interactable = playerGold >= upgradeCost; // Chỉ bật nút khi đủ vàng
    }
}
