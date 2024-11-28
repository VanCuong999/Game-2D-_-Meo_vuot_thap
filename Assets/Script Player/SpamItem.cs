using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamItem : MonoBehaviour
{
    public GameObject item1; 
    public GameObject item2;

    public float spawnRange = 10f;
    void Start()
    {
        Invoke("SpawnItem", 5f);
    }

    void SpawnItem()
    {
        // Chọn ngẫu nhiên một trong hai vật phẩm
        GameObject itemToSpawn = Random.Range(0, 2) == 0 ? item1 : item2;

        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnRange, spawnRange),
            0f, // Giữ cho kẻ thù không xuất hiện trên mặt đất
            Random.Range(-spawnRange, spawnRange)
        );

        // Spawn vật phẩm
        Instantiate(itemToSpawn, randomPosition, Quaternion.identity);
    }

}
