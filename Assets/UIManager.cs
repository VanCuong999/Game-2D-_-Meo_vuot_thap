using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Staft staft;
    public static UIManager Intance;
    [SerializeField] private GameObject overbanner;
    [SerializeField] private GameObject victorybanner;

    private void Awake() 
    {
        Intance = this;    
    }
    

    void Update()
    {
        
    }
    public void ActiveOver()
    {
        overbanner.SetActive(true);
    }
    public void ActiveVictory()
    {
        victorybanner.SetActive(true);
    }
    public void LoadScenes()
    {
        Scene currentScene = SceneManager.GetActiveScene(); // Lấy Scene hiện tại
        SceneManager.LoadScene(currentScene.name);
    }
    public void LoadHome()
    {
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        staft.unlocedLevel +=1;
        
        SceneManager.LoadScene("Level SelecetMenu");
    }
}
