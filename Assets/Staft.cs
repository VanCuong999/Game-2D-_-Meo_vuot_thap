using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Statf")]
public class Staft : ScriptableObject
{
    [Header("Thuộc Tính")]
    public float NangLuong;
    public float Coin;
    public float HuyHieu;

    [Header("Level")]
    public int totalLevel;
    public int unlocedLevel;
    [Header("---------Shop---------")]
    [Header("Coin")]
    public float sovangkhoidau;
    public float sovangtieptheo;
    public float sovangnangcap;

    [Header("Damgage")]
    public float sodamgagekhoidau;
    public float sodamgagetieptheo;
    public float sodamgagenangcap;    

}
