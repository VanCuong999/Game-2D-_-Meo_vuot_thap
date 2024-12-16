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

    [SerializeField] private TextMeshProUGUI soMauStart;
    [SerializeField] private TextMeshProUGUI soMauNext;
    [SerializeField] private TextMeshProUGUI soCoinUPdateMau;

    [Header("Skill")]
    [SerializeField] private TextMeshProUGUI soHHmuaSkillCauLua;
    [SerializeField] private TextMeshProUGUI solansudungSkillCauLua;
    [SerializeField] private TextMeshProUGUI soHHmuaSkillBang;
    [SerializeField] private TextMeshProUGUI solansudungSkillBang;


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

        soMauStart.text = "" +staft.soMauStart;
        soMauNext.text = "" +staft.soMauNext;
        soCoinUPdateMau.text = "" +staft.soCoinUPdateMau;


        soHHmuaSkillCauLua.text = "" +staft.soHHmuaCauLua;
        soHHmuaSkillBang.text = "" + staft.soHHmuaSkillBang;
        solansudungSkillCauLua.text = "x" + staft.solansudungCauLua;
        solansudungSkillBang.text = "x" + staft.solansudungSkillBang;
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
    public void btnShopMau()
    {
        staft.soMauStart += 1;
        staft.soMauNext = staft.soMauNext + 1;
        staft.soCoinUPdateMau += Random.Range(150,200);
        staft.Coin -= staft.soCoinUPdateMau;
    }
    public void AddCoin(int amount)
    {
        Coin += amount; // Tăng vàng cho người chơi
        UpdateManager(NangLuong, Coin, HuyHieu); // Cập nhật lại UI
    }
   
    public void BtnMuaSkillCauLua()
    {
        staft.solansudungCauLua += 1;
        staft.HuyHieu -= staft.soHHmuaCauLua;
    }
    public void BtnMuaSkillBang()
    {
        staft.solansudungSkillBang += 1;
        staft.HuyHieu -= staft.soHHmuaSkillBang;
    }
}
