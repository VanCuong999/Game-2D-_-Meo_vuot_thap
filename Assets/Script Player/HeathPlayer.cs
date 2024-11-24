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
        Heath -= heath;
        UPdateHeath(Heath,maxHeath);

        if (minHeath <= 0)
        {
            Character.Intance.ZeroVelocity();
            Character.Intance.KichHoatDie();
            
        }
        else if(minHeath < 0)
        {
            minHeath = 0;
        }
        
    }

    public void HoiMau(float heath)
    {
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
    }

    public void UPdateHeath(float minHeaths, float maxHeaths)
    {
        minHeath = minHeaths;
        maxHeath = maxHeaths;
    }

}
