using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Exp : MonoBehaviour
{
    public static Exp Intance;
    public float ExpStart;
    public float ExpEnd;
    public float exp;

    [SerializeField] private TextMeshProUGUI expStartText;
    [SerializeField] private TextMeshProUGUI expEndText;
    [SerializeField] private Image  expImage;

    private void Awake() 
    {
        Intance = this;    
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        expImage.fillAmount = Mathf.Lerp(expImage.fillAmount,ExpStart/ExpEnd,10 * Time.deltaTime);

        expStartText.text = $"{ExpStart}";
        expEndText.text = $"{ExpEnd}";
    }

    public void TakeExp(float expcong)
    {
        ExpStart += expcong;
        if (ExpStart >= ExpEnd)
        {
            UIManager.Intance.ActiveVictory();
        }
        UpdateExp(ExpStart,ExpEnd);
    }

    public void UpdateExp(float expStarts , float expEnds)
    {
        ExpStart = expStarts;
        ExpEnd = expEnds;
    }
}
