using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalControner : MonoBehaviour
{
    public Transform destination;
    private GameObject player;
    Animation anim;
    Rigidbody2D playerRb;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animation>();
        playerRb = player.GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            if (Vector2.Distance(player.transform.position,transform.position) > 0.3f)
            {
                player.transform.position = destination.transform.position;
            }
        }    
    }
    
}
