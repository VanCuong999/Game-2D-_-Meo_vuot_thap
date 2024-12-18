using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI textMesh;
    public string[] dialogue; // Câu thoại của NPC
    private int index; // Chỉ số câu thoại
    public GameObject Button;
    public float wordSpeed;
    public bool playerIsClose;

    // Quản lý trạng thái nhiệm vụ
    private enum QuestState { NotStarted, InProgress, Completed }
    private QuestState questState = QuestState.NotStarted;

    public int requiredKills = 5; // Số lượng quái vật cần tiêu diệt
    private int currentKills = 0; // Số lượng quái vật đã tiêu diệt của người chơi
    private bool isTyping = false; // Kiểm tra xem có đang gõ văn bản hay không

    // Thêm tham chiếu đến văn bản hiển thị số lượng quái vật tiêu diệt
    public TextMeshProUGUI killsText; // Tham chiếu đến văn bản hiển thị số lượng tiêu diệt

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                CloseDialogue();
            }
            else
            {
                ShowDialogue();
            }
        }
    }

    void ShowDialogue()
    {
        Debug.Log($"Quest State before dialogue: {questState}");

        dialoguePanel.SetActive(true);
        Button.SetActive(false); // Tắt nút mặc định

        switch (questState)
        {
            case QuestState.NotStarted:
                StartCoroutine(Typing(0, 4)); // Hiển thị 5 câu thoại đầu tiên
                break;

            case QuestState.InProgress:
                StartCoroutine(Typing(5, 5)); // Hiển thị câu thoại số 6
                Button.SetActive(false); // Tắt nút khi chưa hoàn thành nhiệm vụ
                UpdateKillsUI(); // Cập nhật giao diện hiển thị số lượng tiêu diệt khi nhiệm vụ đang tiến hành
                break;

            case QuestState.Completed:
                StartCoroutine(Typing(6, 6)); // Hiển thị câu thoại số 7
                Button.SetActive(false); // Tắt nút Next khi đã hoàn thành nhiệm vụ
                break;
        }
    }

    public void NextLine()
    {
        if (!isTyping) // Chỉ cho phép chuyển dòng khi không đang gõ
        {
            Button.SetActive(false);

            // Không thay đổi index nếu nhiệm vụ đã hoàn thành
            if (index < dialogue.Length - 1 && questState != QuestState.Completed)
            {
                index++;
                textMesh.text = "";
                StartCoroutine(Typing(index, index));
            }
            else
            {
                CloseDialogue();
            }

            // Chuyển trạng thái sang "InProgress" sau 5 dòng đầu tiên
            if (questState == QuestState.NotStarted && index == 4)
            {
                questState = QuestState.InProgress;
                Debug.Log("Quest Started!");
                ShowKillNotification(); // Hiển thị thông báo số lượng quái vật cần tiêu diệt ngay khi nhiệm vụ bắt đầu
            }
        }
    }

    IEnumerator Typing(int startLine, int endLine)
    {
        isTyping = true; // Đánh dấu bắt đầu gõ
        textMesh.text = ""; // Xóa văn bản cũ
        foreach (char letter in dialogue[startLine].ToCharArray())
        {
            textMesh.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        isTyping = false; // Đánh dấu kết thúc gõ

        // Chỉ bật nút khi nhiệm vụ không ở trạng thái "InProgress" hoặc "Completed"
        if (questState != QuestState.InProgress && questState != QuestState.Completed)
        {
            Button.SetActive(true);
        }
        else
        {
            Button.SetActive(false); // Tắt nút khi nhiệm vụ hoàn thành
        }
    }

    void CloseDialogue()
    {
        textMesh.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    // Giả lập cập nhật số lượng quái vật đã tiêu diệt
    public void UpdateKills(int kills)
    {
        currentKills += kills;

        // Log số lượng kills hiện tại và yêu cầu
        Debug.Log($"Current Kills: {currentKills}, Required Kills: {requiredKills}");

        if (currentKills >= requiredKills && questState == QuestState.InProgress)
        {
            questState = QuestState.Completed;  // Cập nhật trạng thái nhiệm vụ
            Debug.Log("Quest Completed!");  // Log khi nhiệm vụ hoàn thành
        }

        // Log trạng thái questState sau khi cập nhật
        Debug.Log($"Quest State after update: {questState}");

        // Cập nhật giao diện người dùng với số lượng tiêu diệt
        UpdateKillsUI();
    }

    // Cập nhật giao diện số lượng tiêu diệt
    void UpdateKillsUI()
    {
        if (questState == QuestState.InProgress)
        {
            killsText.text = $"Kills: {currentKills}/{requiredKills}"; // Cập nhật số lượng tiêu diệt trên giao diện
        }
    }

    // Hiển thị thông báo số lượng quái vật cần tiêu diệt khi nhiệm vụ bắt đầu
    void ShowKillNotification()
    {
        killsText.text = $"Mission started! Kill {requiredKills} monsters."; // Hiển thị thông báo khi bắt đầu nhiệm vụ
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsClose = false;
            CloseDialogue();
        }
    }
}
