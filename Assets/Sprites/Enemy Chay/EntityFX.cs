using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;
    [Header("Flash info")]
    [SerializeField] private Material material;
    [SerializeField] private float flashDursion;
    private Material originMat;

    private void Start() 
    {
        sr = GetComponent<SpriteRenderer>();
        originMat = sr.material;    
    }

    private IEnumerator FlashFX()
    {
        sr.material = material;
        yield return new WaitForSeconds(flashDursion);
        sr.material = originMat;
    }
}
