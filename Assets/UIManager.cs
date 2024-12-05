using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Staft staft;
    public static UIManager Intance;
    [SerializeField] private GameObject overbanner;
    [SerializeField] private GameObject victorybanner;

    [SerializeField] private TextMeshProUGUI sovangkhoidauTMP;
    [SerializeField] private TextMeshProUGUI sovangtieptheoTMP;
    [SerializeField] private TextMeshProUGUI sovangnangcaTMP;

    [SerializeField] private TextMeshProUGUI sodamagekhoidauTMP;
    [SerializeField] private TextMeshProUGUI sodamagetieptheoTMP;
    [SerializeField] private TextMeshProUGUI sodamagenangcapTMP;

    private void Awake()
    {
        Intance = this;
    }


    void Update()
    {
        sovangkhoidauTMP.text = " " + staft.sovangkhoidau;
        sovangtieptheoTMP.text = " " + staft.sovangtieptheo;
        sovangnangcaTMP.text = " " + staft.sovangnangcap;

        sodamagekhoidauTMP.text = "" + staft.sodamgagekhoidau;
        sodamagetieptheoTMP.text = " " + staft.sodamgagetieptheo;
        sodamagenangcapTMP.text = " " + staft.sodamgagenangcap;

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
    public void btnshopcoin()
    {
        staft.sovangkhoidau += 2;
        staft.sovangtieptheo = staft.sovangkhoidau + 2;
        staft.sovangnangcap += Random.Range(50,100);
        staft.Coin -= staft.sodamgagenangcap;
    }
    
    public void btnShopDamage()
    {
        staft.sodamgagekhoidau += 5;
        staft.sodamgagetieptheo = staft.sodamgagekhoidau + 5;
        staft.sodamgagenangcap += Random.Range(50,100);
        staft.Coin -= staft.sodamgagenangcap;
    }
}
