using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpawnerEnemy : MonoBehaviour
{

    [SerializeField]
    private GameObject EnemyFrefab;

    [SerializeField]
    private float _MinimunSpawnTime;

    [SerializeField]
    private float _MaximunSpawnTime;

    private float _TimeUnilSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _TimeUnilSpawn -= Time.deltaTime;
        if(_TimeUnilSpawn <= 0)
        {
            Instantiate(EnemyFrefab,transform.position,Quaternion.identity);
            setTimeUntilSpawn();
        }
    }
    private void setTimeUntilSpawn()
    {
        _TimeUnilSpawn = Random.Range(_MinimunSpawnTime, _MaximunSpawnTime);
    }
}
