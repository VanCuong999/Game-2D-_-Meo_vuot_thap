using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Intance;
    public Staft staft;
    public TextMeshProUGUI NangLuongTMP;
    public TextMeshProUGUI CoinTMP;
    public TextMeshProUGUI HuyHieuTMP;


    private float NangLuong;
    private float Coin;
    private float HuyHieu;
    private void Awake() 
    {
        Intance = this;    
    }
    void Start()
    {
        NangLuong = staft.NangLuong ;
        Coin =  staft.Coin;
        HuyHieu = staft.HuyHieu;
        UpdateManager(NangLuong,Coin,HuyHieu);
    }

    // Update is called once per frame
    void Update()
    {
        NangLuongTMP.text = "" + NangLuong;
        CoinTMP.text = "" + Coin;
        HuyHieuTMP.text = "" + HuyHieu;
        
        
    }

    public void UpdateManager(float nangluong, float coin, float huyhieu)
    {
        NangLuong = nangluong;
        Coin = coin;
        HuyHieu = huyhieu;
    }
    public void LoadLevelMenu()
    {
        SceneManager.LoadScene("Level SelecetMenu");
    }
}
