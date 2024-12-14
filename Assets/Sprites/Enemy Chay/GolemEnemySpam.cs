using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemEnemySpam : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab của kẻ thù
    public float spawnRange = 10f;  // Khu vực spawn
    public float spawnInterval = 5f; // Thời gian giữa các lần spawn (đã sửa thành 5 giây)

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Tạo vị trí spawn ngẫu nhiên
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnRange, spawnRange),
            0f, // Giữ cho kẻ thù không xuất hiện trên mặt đất
            Random.Range(-spawnRange, spawnRange)
        );

        // Spawn kẻ thù
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
