using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeathPlayer : MonoBehaviour
{
    public static HeathPlayer Intance;
    public float minHeath;
    public float maxHeath;

    private float Heath;

    [SerializeField] private Image heathImage;
    [SerializeField] private TextMeshProUGUI heathText;
    [SerializeField] protected GameObject floatingTextPrefab;
    private void Awake() 
    {
        Intance = this;    
    }
    private void Start() 
    {
        Heath = minHeath;
        UPdateHeath(Heath,maxHeath);    
    }
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeHeath(10);
        }
        heathImage.fillAmount = Mathf.Lerp(heathImage.fillAmount,minHeath/maxHeath,10 * Time.deltaTime);
        heathText.text = $"{minHeath} / {maxHeath}";
    }

    public void TakeHeath(float heath)
    {
        ShowDamage(heath.ToString());
        Heath -= heath;
        if (minHeath <= 0)
        {
            Character.Intance.ZeroVelocity();
            Character.Intance.KichHoatDie();
            
        }
        UPdateHeath(Heath,maxHeath);
    }

    public void HoiMau(float heath)
    {
        ShowDamage(heath.ToString());
        Heath+= heath;
        UPdateHeath(Heath,maxHeath);
        if (Heath > maxHeath)
        {
            Heath = maxHeath;
        }
    }
    public void UPLevelHeath()
    {
        minHeath += 30;
        maxHeath += 30;
        UPdateHeath(Heath,maxHeath);
    }

    public void UPdateHeath(float minHeaths, float maxHeaths)
    {
        minHeath = minHeaths;
        maxHeath = maxHeaths;
    }

    public void ShowDamage(string text)
    {
        if (floatingTextPrefab)
        {
            GameObject prefab = Instantiate(floatingTextPrefab, Character.Intance.transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;

            Destroy(prefab, 1f);
        }
    }

}
