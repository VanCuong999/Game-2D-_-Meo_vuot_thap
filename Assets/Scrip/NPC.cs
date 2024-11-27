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
        "Chào mừng, lữ khách! Ngươi có sẵn sàng cho một nhiệm vụ không?",
        "Nhiệm vụ của ngươi là vào hầm ngục và lấy Vòng cổ Thời Gian.",
        "Cẩn thận, hầm ngục đầy rẫy nguy hiểm. Chúc ngươi may mắn!"
    };
    private int dialogueIndex = 0; // Vị trí hiện tại trong hội thoại

    private bool isPlayerNearby = false; // Kiểm tra người chơi có gần NPC không
    private bool isDialogueActive = false; // Trạng thái hội thoại đang diễn ra

    void Start()
    {
        // Ẩn thông báo khi bắt đầu
        interactText.gameObject.SetActive(false);
        dialoguePanel.SetActive(false); // Ẩn panel hội thoại khi bắt đầu
        dialogueBackground.SetActive(false); // Ẩn khung nền hội thoại
    }

    void Update()
    {
        // Nếu Player ở gần NPC và nhấn phím E để tương tác
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
            dialogueText.text = dialogues[dialogueIndex]; // Hiển thị câu thoại tiếp theo
        }
        else
        {
            EndDialogue(); // Kết thúc nếu hết hội thoại
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
            interactText.text = "Nhấn E để đọc thông tin";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactText.gameObject.SetActive(true);
            interactText.text = "Nhấn E để đọc thông tin";
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
}
