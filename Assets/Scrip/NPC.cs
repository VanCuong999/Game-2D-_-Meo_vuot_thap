using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NPC : MonoBehaviour
{
    [Header("UI Components")]
    public GameObject dialoguePanel; // UI Panel chứa hội thoại
    public TextMeshProUGUI dialogueText; // Text UI để hiển thị nội dung hội thoại
    public TextMeshProUGUI interactText; // Text UI để hiển thị thông báo nhấn E
    public GameObject dialogueBackground; // Khung nền hội thoại

    [Header("Dialogue Content")]
    public string[] dialogues = {
    
};
    private int dialogueIndex = 0; // Vị trí hiện tại trong hội thoại

    private bool isPlayerNearby = false; // Kiểm tra người chơi có gần NPC không
    private bool isDialogueActive = false; // Trạng thái hội thoại đang diễn ra


   // [Header("Quest System")]
   /// public int requiredKills = 3; // Số lượng quái cần tiêu diệt
    //private int currentKills = 0; // Số lượng quái đã tiêu diệt
    //private bool questCompleted = false; // Trạng thái nhiệm vụ
    //private bool rewardGiven = false; // Kiểm tra đã nhận thưởng hay chưa

    //[Header("Quest Guidance")]
    //public Transform enemyArea; // Vị trí quái vật
    //public GameObject arrow; // Mũi tên hướng dẫn (được gắn trên Player)
    //public float detectionRadius = 2f; // Bán kính để tắt mũi tên khi gần khu vực quái vật

    //[Header("Rewards")]
    //public int rewardAmount = 100; // Phần thưởng (ví dụ: vàng, điểm kinh nghiệm)

    void Start()
    {
        // Ẩn thông báo khi bắt đầu
        interactText.gameObject.SetActive(false);
        dialoguePanel.SetActive(false); // Ẩn panel hội thoại khi bắt đầu
        dialogueBackground.SetActive(false); // Ẩn khung nền hội thoại
        //arrow.SetActive(false); // Ẩn mũi tên ban đầu
    }

    void Update()
    {
        // Nếu Player ở gần NPC và nhấn phím E để tương tác
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialogue();
        }
      /*  if (arrow.activeSelf)
        {
            UpdateArrowDirection();
            CheckProximityToEnemyArea(); // Kiểm tra nếu Player đã gần khu vực quái
        }*/
    }

    void TriggerDialogue()
    {
        if (!isDialogueActive)
        {
            StartDialogue();
        }
        else
        {
            if (dialogueIndex < dialogues.Length)
            {
                ContinueDialogue();
            }
            else
            {
                EndDialogue(); // Kết thúc nếu hết hội thoại
            }
        }
    }

    void StartDialogue()
    {
        if (dialogues.Length > 0)
        {
            dialoguePanel.SetActive(true); // Hiển thị Panel
            dialogueBackground.SetActive(true); // Hiển thị khung nền hội thoại
            dialogueText.text = dialogues[dialogueIndex]; // Hiển thị câu thoại đầu tiên
            isDialogueActive = true;
            interactText.gameObject.SetActive(false); // Ẩn thông báo nhấn E
        }
        else
        {
            Debug.LogWarning("Không có câu thoại nào trong mảng dialogues.");
        }
    }

    void ContinueDialogue()
    {
        dialogueIndex++;
        if (dialogueIndex < dialogues.Length)
        {
            //if (dialogueIndex == 1 && !questCompleted)
            //{
               // arrow.SetActive(true); // Hiển thị mũi tên hướng dẫn
            //}

            // Kiểm tra nếu chưa hoàn thành nhiệm vụ
            /*if (dialogueIndex == 2 && !questCompleted)
            {
                dialogueText.text = "ban chua hoan thanh nhiem vu";
                dialogueIndex--; // Giữ lại hội thoại hiện tại (không chuyển tiếp)
                return;
            }

            // Kiểm tra nếu đã hoàn thành nhưng chưa nhận thưởng
            if (dialogueIndex == 2 && questCompleted && !rewardGiven)
            {
                dialogueText.text = dialogues[2]; // Hiển thị lời cảm ơn
                GiveReward(); // Trao thưởng
                rewardGiven = true; // Đánh dấu đã nhận thưởng
                return;
            }

            // Hiển thị lời chào sau khi trả nhiệm vụ
            if (dialogueIndex == 3 && rewardGiven)
            {
                dialogueText.text = dialogues[3];
            }
            else
            {
                dialogueText.text = dialogues[dialogueIndex];
            }*/
        }
        else
        {
            EndDialogue(); // Kết thúc hội thoại
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false); // Ẩn Panel
        dialogueBackground.SetActive(false); // Ẩn khung nền
        dialogueText.text = "";
        dialogueIndex = 0; // Reset hội thoại
        isDialogueActive = false;

        if (isPlayerNearby)
        {
            interactText.gameObject.SetActive(true);
            interactText.text = "E";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactText.gameObject.SetActive(true);
            interactText.text = "E";
            Debug.Log("Nhấn E để nói chuyện với NPC.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactText.gameObject.SetActive(false);
            Debug.Log("Người chơi đã rời xa NPC.");
            EndDialogue();
        }
    }
   /* public void UpdateQuestProgress()
    {
        currentKills++;
        Debug.Log($"Quái vật đã tiêu diệt: {currentKills}/{requiredKills}");

        if (currentKills >= requiredKills)
        {
            questCompleted = true;
            Debug.Log("Nhiệm vụ hoàn thành! Hãy quay lại gặp NPC để nhận thưởng.");
            arrow.SetActive(false);
        }
    }*/
   /* void GiveReward()
    {
        Debug.Log($"Bạn đã nhận được {rewardAmount} vàng!");
        // Thêm logic trao phần thưởng ở đây (ví dụ: cập nhật tiền, kinh nghiệm, v.v.)
    }
    void UpdateArrowDirection()
    {
        Vector3 direction = (enemyArea.position - arrow.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    void CheckProximityToEnemyArea()
    {
        float distanceToEnemyArea = Vector3.Distance(transform.position, enemyArea.position);
        if (distanceToEnemyArea <= detectionRadius)
        {
            arrow.SetActive(false); // Tắt mũi tên khi đến gần khu vực quái
            Debug.Log("Bạn đã đến khu vực đánh quái.");
        }
    }*/
}
