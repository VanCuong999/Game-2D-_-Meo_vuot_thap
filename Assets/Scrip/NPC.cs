using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI textMesh;
    public string[] dialogue; // NPC dialogues
    private int index; // Dialogue index
    public GameObject Button;
    public float wordSpeed;
    public bool playerIsClose;

    // Quest State Management
    private enum QuestState { NotStarted, InProgress, Completed }
    private QuestState questState = QuestState.NotStarted;

    public int requiredKills = 5; // Required monster kills
    private int currentKills = 0; // Player's current kills
    private bool isTyping = false; // Check if text is being typed

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
            }
        }
    }


    IEnumerator Typing(int startLine, int endLine)
    {
        isTyping = true; // Mark typing as active
        textMesh.text = ""; // Clear previous text
        foreach (char letter in dialogue[startLine].ToCharArray())
        {
            textMesh.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        isTyping = false; // Mark typing as finished

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

    // Simulate updating monster kills
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
