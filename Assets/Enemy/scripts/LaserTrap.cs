using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    public float activationDelay = 3f;  
    public int damage = 20;             
    public Animator laserAnimator;      
    private BoxCollider2D laserCollider; 

    private bool isActive = false;     

    void Start()
    {
        laserCollider = GetComponent<BoxCollider2D>(); 
        laserCollider.enabled = false;  
        StartCoroutine(ActivateLaserCycle());
    }
    private IEnumerator ActivateLaserCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(activationDelay); 

            isActive = true; 
            laserCollider.enabled = true;  
            laserAnimator.SetTrigger("LaserAttack");
            yield return new WaitForSeconds(3f); 
            laserAnimator.SetTrigger("LaserIdle");
            yield return new WaitForSeconds(0.5f); 
            isActive = false; 
            laserCollider.enabled = false; 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive && collision.CompareTag("Player"))
        {
            // PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            // if (playerHealth != null)
            // {
            //     playerHealth.TakeDamage(damage);
            // }
            HeathPlayer.Intance.TakeHeath(damage);
        }
    }
}
