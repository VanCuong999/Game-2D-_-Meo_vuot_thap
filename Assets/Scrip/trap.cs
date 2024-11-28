using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    public GameObject meteorPrefab; // Meteor prefab
    public float spawnDuration = 10f; // Tổng thời gian rơi bom (10 giây)
    public float areaWidth = 5f; // Độ rộng khu vực rơi bom xung quanh người chơi
    public float areaHeight = 5f; // Độ cao khu vực rơi bom xung quanh người chơi

    private bool isTrapActivated = false;
    private GameObject player;

    private void Start()
    {
        // Lấy đối tượng người chơi
        player = GameObject.FindWithTag("Player");
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra xem người chơi có dẫm phải bẫy không
        if (other.CompareTag("Player") && !isTrapActivated)
        {
            isTrapActivated = true;
            StartCoroutine(SpawnMeteorsAroundPlayer());
        }
    }

    // Coroutine để spawn meteor xung quanh người chơi trong 10 giây
    private IEnumerator SpawnMeteorsAroundPlayer()
    {
        float elapsedTime = 0f;

        // Trong vòng 10 giây, spawn meteor mỗi giây
        while (elapsedTime < spawnDuration)
        {
            SpawnMeteorAroundPlayer(); // Gọi hàm spawn meteor xung quanh người chơi
            elapsedTime += 1f;
            yield return new WaitForSeconds(1f); // Chờ 1 giây trước khi spawn meteor tiếp theo
        }
    }

    // Hàm để spawn meteor xung quanh người chơi
    private void SpawnMeteorAroundPlayer()
    {
        if (player != null)
        {
            // Tạo tọa độ ngẫu nhiên xung quanh người chơi
            float randomX = Random.Range(-6f, 6f); // Random theo chiều ngang
            float fixedY = 15f; // Chiều cao cố định
            Vector3 spawnPosition = player.transform.position + new Vector3(randomX, fixedY, 0f);

            // Instantiate meteor prefab tại vị trí ngẫu nhiên
            Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(player.transform.position, new Vector3(30f, 30f, 0f)); // Hiển thị khu vực bom rơi
        }
    }
}
