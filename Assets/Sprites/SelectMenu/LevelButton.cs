using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{

    public LevelSelectMenu menu;
    public Sprite lockSprite;
    public Sprite startSprite;
    public TextMeshProUGUI levelText;
    private int level = 0;
    private Button button;
    private Image image;
    
    private void OnEnable() 
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    public void SetUp(int level, bool isUnlock)
    {
        this.level = level;
        levelText.text = level.ToString();

        if (isUnlock)
        {
            image.sprite = startSprite;
            button.enabled = true;
            levelText.gameObject.SetActive(true);
        }
        else
        {
            image.sprite = lockSprite;
            button.enabled = false;
            levelText.gameObject.SetActive(false);
        }
    }
    public void OnClick()
    {
        menu.StartLevel(level);
    }
}
