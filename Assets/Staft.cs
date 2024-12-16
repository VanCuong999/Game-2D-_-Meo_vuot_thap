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

    [Header("Số máu hồi phục")]
    public float soMauStart;
    public float soMauNext;
    public float soCoinUPdateMau;

    [Header("--------Skill-----------")]
    [Header("Skill Cầu Lửa")]
    public float soHHmuaCauLua;
    public float solansudungCauLua;

    [Header("Skill Băng")]
    public float soHHmuaSkillBang;
    public float solansudungSkillBang;

}
