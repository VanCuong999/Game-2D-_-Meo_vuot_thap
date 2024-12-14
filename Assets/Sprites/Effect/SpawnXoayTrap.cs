using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnXoayTrap : MonoBehaviour
{
    public GameObject trapPrefab; // prefab cho bẫy
    public int spawnCount = 5; // Số lượng bẫy cần spawn
    public Vector2 spawnAreaMin; // Điểm dưới bên trái của khu vực spawn
    public Vector2 spawnAreaMax; // Điểm trên bên phải của khu vực spawn

    void Start()
    {
        StartCoroutine(SpawnTrapsRoutine());
    }

    IEnumerator SpawnTrapsRoutine()
    {
        while (true) // Vòng lặp vô hạn
        {
            SpawnTraps();
            yield return new WaitForSeconds(5f); // Chờ 5 giây
        }
    }

    void SpawnTraps()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            // Tạo vị trí ngẫu nhiên trong khu vực spawn
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector2 spawnPosition = new Vector2(randomX, randomY);

            // Spawn bẫy
            GameObject spawnedTrap = Instantiate(trapPrefab, spawnPosition, Quaternion.identity);

            // Xóa bẫy sau 2 giây
            Destroy(spawnedTrap, 2f);
        }
    }
}
