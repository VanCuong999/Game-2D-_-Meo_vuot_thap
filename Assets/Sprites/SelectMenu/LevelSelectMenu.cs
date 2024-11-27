using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour
{
    public Staft staft;
    public static LevelSelectMenu Intance;
    
    private LevelButton[] levelButtons;
    private int totalPage = 0;
    private int page;
    private int pageItem = 6;

    private void Awake() 
    {
        Intance = this;    
    }
    private void Start() 
    {
        Refresh();    
    }
    private void OnEnable() {
        levelButtons = GetComponentsInChildren<LevelButton>();
    }

    public void StartLevel(int level)
    {
        SceneManager.LoadScene(1 + level);
    }
    
    public void Refresh()
    {
        totalPage = staft.totalLevel / pageItem;
        int index = page * pageItem;

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int level = index + i + 1;
            if (level <= staft.totalLevel)
            {
                levelButtons[i].gameObject.SetActive(true);
                levelButtons[i].SetUp(level, level<= staft.unlocedLevel);
            }
            else levelButtons[i].gameObject.SetActive(false);

        }
    }
}
