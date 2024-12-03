using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public static EnemyShoot Instance;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletsPos;

    [SerializeField] private float Sice;

    public float HeathEnemy;
    private Transform player;

    private float timer;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            timer = 0;
            Shoot();
        }
    }
    public void Shoot()
    {
        float Distance = Vector2.Distance(player.position, transform.position);
        if (Distance < Sice)
        {
            Instantiate(bullet, bulletsPos.position, Quaternion.identity);
        }

    }
    public void TakeDamge(float damge)
    {
        HeathEnemy -= damge;
        
        UpdateHeathUFO(HeathEnemy);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Sice);
    }

    public void UpdateHeathUFO(float health)
    {
        HeathEnemy = health;
    }
}
