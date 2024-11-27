using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ThanhTuu : MonoBehaviour
{
    public Staft staft;
    public GameObject btntuu1;
    public GameObject btntuu2;
    public GameObject btntuu3;
    void Start()
    {
        DieuKienThanhTuu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tuu1()
    {
        staft.Coin += 100;
        GameManager.Intance.UpdateManager(staft.NangLuong,staft.Coin,staft.HuyHieu);
        btntuu1.SetActive(false);
        
    }
    public void Tuu2()
    {
        staft.Coin += 200;
        GameManager.Intance.UpdateManager(staft.NangLuong,staft.Coin,staft.HuyHieu);
        btntuu2.SetActive(false);
        
    }
    public void Tuu3()
    {
        staft.HuyHieu += 200;
        GameManager.Intance.UpdateManager(staft.NangLuong,staft.Coin,staft.HuyHieu);
        btntuu3.SetActive(false);
        
    }

    public void DieuKienThanhTuu()
    {
        if (staft.unlocedLevel > 1)
        {
            btntuu1.SetActive(true);
        }
        if (staft.unlocedLevel > 2)
        {
            btntuu2.SetActive(true);
        }
    }
}
