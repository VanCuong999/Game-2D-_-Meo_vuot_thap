using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NPC : MonoBehaviour
{
    [Header("UI Components")]
    public GameObject dialoguePanel; // UI Panel cho hội thoại
    public TextMeshProUGUI dialogueText; // Text để hiển thị nội dung hội thoại
    public TextMeshProUGUI interactText; // Text để hiển thị thông báo "Nhấn E"
    public GameObject dialogueBackground; // Nền của hội thoại

    [Header("Dialogue Content")]
    public string[] dialogues = {
        "Chào bạn, tôi cần bạn giúp tôi tiêu diệt quái vật.", // Câu 1
        "Hãy cố gắng hoàn thành nhiệm vụ nhé!", // Câu 2
        "Bạn chưa hoàn thành nhiệm vụ.", // Câu 3
        "Cảm ơn bạn đã hoàn thành nhiệm vụ." // Câu 4
    };

    private bool isPlayerNearby = false; // Kiểm tra nếu người chơi gần NPC
    private bool isDialogueActive = false; // Trạng thái hội thoại đang diễn ra
    private bool questAccepted = false; // Kiểm tra người chơi đã nhận nhiệm vụ chưa
    private bool questCompleted = false; // Kiểm tra nhiệm vụ đã hoàn thành chưa

    private int dialogueIndex = 0; // Để theo dõi chỉ số của câu thoại hiện tại

    [Header("Quest System")]
    public int requiredKills = 3; // Số quái cần tiêu diệt
    private int currentKills = 0; // Số quái đã tiêu diệt

    [Header("Reward UI")]
    public GameObject rewardButton;
    public TextMeshProUGUI rewardAmountText;
    public int rewardAmount = 100;

    void Start()
    {
        interactText.gameObject.SetActive(false);
        dialoguePanel.SetActive(false);
        dialogueBackground.SetActive(false);
        rewardButton.SetActive(false); // Ẩn nút phần thưởng
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialogue();
        }
    }

    void TriggerDialogue()
    {
        if (!isDialogueActive)
        {
            StartDialogue();
        }
        else
        {
            ContinueDialogue();
        }
    }

    void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        dialogueBackground.SetActive(true);

        // Ẩn thông báo "Nhấn E" khi hội thoại bắt đầu
        interactText.gameObject.SetActive(false);

        if (!questAccepted) // Nếu người chơi chưa nhận nhiệm vụ
        {
            dialogueText.text = dialogues[0]; // Hiển thị câu 1
            dialogueIndex = 1; // Chuyển chỉ số câu thoại sang 1
            questAccepted = true; // Đánh dấu nhiệm vụ đã được nhận
        }
        else if (!questCompleted) // Nếu nhiệm vụ chưa hoàn thành
        {
            dialogueText.text = dialogues[2]; // Hiển thị câu "Bạn chưa hoàn thành nhiệm vụ."
            dialogueIndex = 3; // Chuyển chỉ số câu thoại sang 3
        }
        else // Nếu nhiệm vụ đã hoàn thành
        {
            dialogueText.text = dialogues[3]; // Hiển thị câu "Cảm ơn bạn đã hoàn thành nhiệm vụ."
        }

        isDialogueActive = true;
    }

    void ContinueDialogue()
    {
        if (dialogueIndex == 1) // Nếu đang ở câu 1
        {
            dialogueText.text = dialogues[1]; // Hiển thị câu 2
            dialogueIndex = 2; // Chuyển chỉ số câu thoại sang 2
        }
        else if (dialogueIndex == 2) // Nếu đang ở câu 2
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        // Ẩn nền và hộp thoại khi hội thoại kết thúc
        dialoguePanel.SetActive(false);
        dialogueBackground.SetActive(false);

        isDialogueActive = false;

        // Hiển thị lại thông báo "Nhấn E" nếu người chơi vẫn ở gần NPC
        if (isPlayerNearby)
        {
            interactText.gameObject.SetActive(true);
            interactText.text = "Nhấn E để nói chuyện";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;

            // Chỉ hiển thị "Nhấn E" nếu hội thoại chưa diễn ra
            if (!isDialogueActive)
            {
                interactText.gameObject.SetActive(true);
                interactText.text = "Nhấn E để nói chuyện";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactText.gameObject.SetActive(false);
            EndDialogue(); // Gọi hàm để tắt nền khi người chơi rời xa
        }
    }

    public void UpdateQuestProgress()
    {
        currentKills++;
        if (currentKills >= requiredKills)
        {
            questCompleted = true; // Đánh dấu nhiệm vụ hoàn thành
            Debug.Log("Nhiệm vụ hoàn thành!");
            rewardButton.SetActive(true);
            rewardAmountText.text = "+" + rewardAmount + " vàng";
        }
    }
    public void ClaimReward()
    {
        GameManager.Intance.AddCoin(rewardAmount);
        rewardButton.SetActive(false);
        Debug.Log("Bạn đã nhận được " + rewardAmount + " vàng!");
    }

}
