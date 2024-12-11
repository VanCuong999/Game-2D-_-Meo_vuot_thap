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

    [Header("Shop")]
    [SerializeField] private TextMeshProUGUI sovangkhoidauTMP;
    [SerializeField] private TextMeshProUGUI sovangtieptheoTMP;
    [SerializeField] private TextMeshProUGUI sovangnangcaTMP;

    [SerializeField] private TextMeshProUGUI sodamagekhoidauTMP;
    [SerializeField] private TextMeshProUGUI sodamagetieptheoTMP;
    [SerializeField] private TextMeshProUGUI sodamagenangcapTMP;
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
        
        sovangkhoidauTMP.text = " " + staft.sovangkhoidau;
        sovangtieptheoTMP.text = " " + staft.sovangtieptheo;
        sovangnangcaTMP.text = " " + staft.sovangnangcap;

        sodamagekhoidauTMP.text = "" + staft.sodamgagekhoidau;
        sodamagetieptheoTMP.text = " " + staft.sodamgagetieptheo;
        sodamagenangcapTMP.text = " " + staft.sodamgagenangcap;
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
    public void AddCoin(int amount)
    {
        Coin += amount; // Tăng vàng cho người chơi
        UpdateManager(NangLuong, Coin, HuyHieu); // Cập nhật lại UI
    }
}
