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



    [System.Serializable]
    public class Dialogue
    {
        public string[] dialogueLines; // Các câu thoại cho mỗi nhiệm vụ
    }

    public List<Dialogue> dialogues = new List<Dialogue>(); // Danh sách hội thoại cho mỗi nhiệm vụ

    private bool isPlayerNearby = false; // Kiểm tra nếu người chơi gần NPC
    private bool isDialogueActive = false; // Trạng thái hội thoại đang diễn ra
    private bool questAccepted = false; // Kiểm tra người chơi đã nhận nhiệm vụ chưa
    private bool questCompleted = false; // Kiểm tra nhiệm vụ đã hoàn thành chưa

    private int dialogueIndex = 0; // Để theo dõi chỉ số của câu thoại hiện tại


    [System.Serializable]
    public class Quest
    {
        public string questName;
        public int requiredKills; // Số quái cần tiêu diệt cho nhiệm vụ này
        public int rewardAmount; // Phần thưởng vàng cho nhiệm vụ này
        public int dialogueIndex; // Chỉ số câu thoại đầu tiên cho nhiệm vụ này
    }

    public List<Quest> quests = new List<Quest>(); // Danh sách các nhiệm vụ
    private int currentQuestIndex = 0; // Chỉ số nhiệm vụ hiện tại
    private int currentKills = 0; // Số quái đã tiêu diệt

    [Header("Reward UI")]
    public GameObject rewardButton;
    public TextMeshProUGUI rewardAmountText;

    public Staft staft;

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
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.O))
        {
            UpdateQuestProgress();
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

        // Nếu nhiệm vụ chưa được nhận
        if (!questAccepted)
        {
            dialogueText.text = dialogues[currentQuestIndex].dialogueLines[0]; // Hiển thị câu đầu tiên của nhiệm vụ
            dialogueIndex = 1; // Chuyển chỉ số câu thoại sang 1
            questAccepted = true; // Đánh dấu nhiệm vụ đã được nhận
        }
        else if (!questCompleted) // Nếu nhiệm vụ chưa hoàn thành
        {
            dialogueText.text = dialogues[currentQuestIndex].dialogueLines[1]; // Hiển thị câu "Bạn chưa hoàn thành nhiệm vụ."
            dialogueIndex = 3; // Chuyển chỉ số câu thoại sang 3
        }
        else // Nếu nhiệm vụ đã hoàn thành
        {
            dialogueText.text = dialogues[currentQuestIndex].dialogueLines[2]; // Hiển thị câu "Cảm ơn bạn đã hoàn thành nhiệm vụ."
            currentQuestIndex++; // Chuyển sang nhiệm vụ tiếp theo
            questAccepted = false;
            questCompleted = true;
        }

        isDialogueActive = true;
    }

    void ContinueDialogue()
    {
        if (dialogueIndex < dialogues[currentQuestIndex].dialogueLines.Length - 1) // Nếu còn câu thoại
        {
            dialogueText.text = dialogues[currentQuestIndex].dialogueLines[dialogueIndex]; // Hiển thị câu tiếp theo
            dialogueIndex++; // Tiến đến câu thoại tiếp theo
        }
        else
        {
            EndDialogue(); // Nếu không còn câu thoại, kết thúc hội thoại
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

            // Hiển thị nhiệm vụ tiếp theo nếu nhiệm vụ trước đã hoàn thành
            if (questCompleted && currentQuestIndex < quests.Count)
            {
                interactText.text = "Nhấn E để nhận nhiệm vụ mới!";
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
        // Kiểm tra nếu chỉ số nhiệm vụ còn hợp lệ
        if (currentQuestIndex >= quests.Count)
        {
            Debug.LogWarning("Chỉ số nhiệm vụ vượt quá phạm vi. Không thể cập nhật tiến độ nhiệm vụ.");
            return; // Dừng lại nếu không có nhiệm vụ hợp lệ
        }

        currentKills++;
        // Kiểm tra nếu người chơi đã tiêu diệt đủ số quái
        if (currentKills >= quests[currentQuestIndex].requiredKills)
        {
            questCompleted = true; // Đánh dấu nhiệm vụ đã hoàn thành
            Debug.Log("Nhiệm vụ hoàn thành!");

            // Hiển thị nút nhận thưởng
            rewardButton.SetActive(true);
            rewardAmountText.text = "+" + quests[currentQuestIndex].rewardAmount + " vàng";
        }
    }


    public void ClaimReward()
    {
        if (staft != null && questCompleted)
        {
            staft.Coin += quests[currentQuestIndex].rewardAmount; // Cộng vàng vào tài khoản người chơi
            rewardButton.SetActive(false); // Ẩn nút nhận thưởng
            Debug.Log("Bạn đã nhận được " + quests[currentQuestIndex].rewardAmount + " vàng!");
        }
        else
        {
            Debug.LogError("Staft is null or quest not completed!");
        }
    }

}