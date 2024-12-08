using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class andomenemy : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyFrefab;  // Prefab của quái vật

    [SerializeField]
    private float _MinimunSpawnTime = 3f;  // Thời gian tối thiểu giữa các lần spawn
    [SerializeField]
    private float _MaximunSpawnTime = 3f;  // Thời gian tối đa giữa các lần spawn (để đảm bảo spawn mỗi 3 giây)

    [SerializeField]
    private float mapWidth = 80f;  // Chiều rộng của bản đồ
    [SerializeField]
    private float mapLength = 160f; // Chiều dài của bản đồ

    private float _TimeUnilSpawn;

    // Start is called before the first frame update
    void Start()
    {
        setTimeUntilSpawn();  // Thiết lập thời gian cho lần spawn đầu tiên
    }

    // Update is called once per frame
    void Update()
    {
        _TimeUnilSpawn -= Time.deltaTime;
        if (_TimeUnilSpawn <= 0)
        {
            SpawnEnemy();  // Gọi phương thức spawn quái vật
            setTimeUntilSpawn();  // Thiết lập lại thời gian cho lần spawn tiếp theo
        }
    }

    private void SpawnEnemy()
    {
        // Tạo một vị trí ngẫu nhiên trong phạm vi của bản đồ
        Vector3 spawnPosition = new Vector3(
            Random.Range(-mapWidth / 2f, mapWidth / 2f),  // Random X trong phạm vi chiều rộng bản đồ
            0f,  // Giữ cho quái vật ở mặt đất (Y = 0)
            Random.Range(-mapLength / 4f, mapLength / 4f) // Random Z trong phạm vi chiều dài bản đồ
        );

        // Spawn quái vật tại vị trí ngẫu nhiên
        Instantiate(EnemyFrefab, spawnPosition, Quaternion.identity);
    }

    private void setTimeUntilSpawn()
    {
        _TimeUnilSpawn = Random.Range(_MinimunSpawnTime, _MaximunSpawnTime);  // Cập nhật thời gian spawn ngẫu nhiên
    }
}