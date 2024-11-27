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

}
