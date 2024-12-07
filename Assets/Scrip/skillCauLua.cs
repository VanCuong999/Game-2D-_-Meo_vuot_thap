using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class skillCauLua : MonoBehaviour
{
    public TextMeshProUGUI playerGemsText; // Dùng TextMesh Pro
    public Button buyFireballButton;
    public TextMeshProUGUI messageText;

    private int fireballCost = 2000;
    private bool hasBoughtFireball = false;

    void Start()
    {
        UpdateUI();
        CheckButtonState();
    }

    public void BuyFireball()
    {
        int playerGems = GetPlayerGems();

        if (hasBoughtFireball)
        {
            messageText.text = "Bạn đã sở hữu kỹ năng Cầu Lửa!";
            return;
        }

        if (playerGems >= fireballCost)
        {
            playerGems -= fireballCost;
            hasBoughtFireball = true;
            SetPlayerGems(playerGems);
            UpdateUI();
            messageText.text = "Bạn đã mua kỹ năng Cầu Lửa!";
        }
        else
        {
            messageText.text = "Không đủ đá tím!";
        }

        CheckButtonState();
    }

    private void UpdateUI()
    {
        playerGemsText.text = "Đá tím: " + GetPlayerGems();
    }

    private void CheckButtonState()
    {
        int playerGems = GetPlayerGems();
        buyFireballButton.interactable = (playerGems >= fireballCost && !hasBoughtFireball);
    }

    private int GetPlayerGems()
    {
        int gems = 0;
        int.TryParse(playerGemsText.text, out gems);
        return gems;
    }

    private void SetPlayerGems(int gems)
    {
        playerGemsText.text = gems.ToString();
    }
}
