using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class expPlayer : MonoBehaviour
{
    public static expPlayer Intancs;
    public int level = 1;
    public int experience = 0;
    public int experienceToLevelUp = 100; // Điểm kinh nghiệm cần để lên cấp
    
    [SerializeField] private Image expPlayerImage;
    [SerializeField] private TextMeshProUGUI expPlayerText;
    private void Awake() 
    {
        Intancs = this;   
    }
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            GainExperience(20);
        }
        expPlayerImage.fillAmount = Mathf.Lerp(expPlayerImage.fillAmount,(float)experience / experienceToLevelUp,10 * Time.deltaTime);
        expPlayerText.text = $"{level}";
        UPdateExpPlayer(experience,experienceToLevelUp);
    }
    public void GainExperience(int amount)
    {
        experience += amount;
        Debug.Log("Kinh nghiệm hiện tại: " + experience);
        UPdateExpPlayer(experience,experienceToLevelUp);
        // Kiểm tra xem có đủ kinh nghiệm để lên cấp không
        while (experience >= experienceToLevelUp)
        {
            experience -= experienceToLevelUp;
            LevelUp();
            
        }
        
    }

    private void LevelUp()
    {
        level++;
        experienceToLevelUp += 50; // Tăng yêu cầu kinh nghiệm cho cấp kế tiếp
        Debug.Log("Đã lên cấp! Cấp độ hiện tại: " + level);
        HeathPlayer.Intance.UPLevelHeath();
    }

    public void UPdateExpPlayer(int experiences, int experienceToLevelUps)
    {
        experience = experiences;
        experienceToLevelUp = experienceToLevelUps;
    }
}
