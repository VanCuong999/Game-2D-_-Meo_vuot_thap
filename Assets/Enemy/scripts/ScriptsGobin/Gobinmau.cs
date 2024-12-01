using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gobinmau : MonoBehaviour
{
    public int health = 100;
    public static Gobinmau Intance;


    private void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }


   
    
    
    void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        Destroy(gameObject);
    }
    public void TakeDamage()
    {
        Debug.Log(gameObject.name + " is taking damage!");
        if (gameObject != null)
        {
            
        }

        health -= 50;

        if (health <= 0)
        {
            Debug.Log("gobin die");
        }
    }
}

