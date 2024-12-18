using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Staft staft;
    public static UIManager Intance;
    [SerializeField] private GameObject overbanner;
    [SerializeField] private GameObject victorybanner;
    [SerializeField] private GameObject pausebanner;

    [SerializeField] private TextMeshProUGUI coinTMP;
    [SerializeField] private TextMeshProUGUI HuyHieuTMP;

    [SerializeField] private GameObject btnCauLua;
    [SerializeField] private GameObject btnSkillBang;


    private void Awake()
    {
        Intance = this;
    }



    void Update()
    {
        coinTMP.text = " " + staft.Coin;
        HuyHieuTMP.text = " " + staft.HuyHieu;

        CheckDKSkillCauLua();
        CheckDKSkillBang();
    }

    public void CheckDKSkillCauLua()
    {
        if (staft.solansudungCauLua > 0)
        {
            btnCauLua.SetActive(true);
        }
        else
        {
            btnCauLua.SetActive(false);
        }
    }

    public void CheckDKSkillBang()
    {
        if (staft.solansudungSkillBang > 0)
        {
            btnSkillBang.SetActive(true);
        }
        else
        {
            btnSkillBang.SetActive(false);
        }
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
        staft.unlocedLevel += 1;

        SceneManager.LoadScene("Level SelecetMenu");
    }

    public void BtnPause()
    {
        Time.timeScale = 0;
        pausebanner.SetActive(true);
    }

    public void Btncontinue()
    {
        Time.timeScale = 1;
        pausebanner.SetActive(false);
    }
}
