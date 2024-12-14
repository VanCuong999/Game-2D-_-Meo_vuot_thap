using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGolem : MonoBehaviour
{
    private float time;
    public float TimeHoi;
    public GameObject enemySpawn;

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= TimeHoi)
        {
            time = 0;
            SpawEnemys();
        }
    }

    public void SpawEnemys()
    {

        Vector3 randowPosition = new Vector3(
          Random.Range(-68, -38),
          Random.Range(14, 40),
          0
        );

        Instantiate(enemySpawn, randowPosition, Quaternion.identity);

    }
}
