using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cauLua : MonoBehaviour
{
    public GameObject fireballPrefab; // Prefab quả cầu lửa
    public Transform spawnPoint;     // Vị trí tạo quả cầu lửa
    public float speed = 5f;         // Tốc độ quả cầu lửa
    public float skillRange = 10f;    // Bán kính phạm vi chiêu

    void Update()
    {
        // Kiểm tra khi nhấn phím "T" và có ít nhất một enemy trong phạm vi
        if (Input.GetKeyDown(KeyCode.T) && IsEnemyInRange())
        {
            CastFireballSkill(); // Kích hoạt chiêu khi nhấn phím "T"
        }
    }

    public void CastFireballSkill()
    {
        // Lấy danh sách các enemy trong phạm vi
        GameObject[] targets = FindEnemiesInRange(skillRange);

        // Nếu có ít nhất một kẻ thù trong phạm vi
        if (targets.Length > 0)
        {
            // Chọn kẻ thù gần nhất
            GameObject target = targets[0];

            // Quay spawnPoint về hướng của kẻ thù
            Vector3 direction = (target.transform.position - spawnPoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            spawnPoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Tạo một quả cầu lửa và nhắm vào mục tiêu
            GameObject fireball = Instantiate(fireballPrefab, spawnPoint.position, spawnPoint.rotation);
            fireball.GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }

    // Kiểm tra xem có ít nhất một enemy nào trong phạm vi chiêu không
    private bool IsEnemyInRange()
    {
        GameObject[] enemies = FindEnemiesInRange(skillRange);
        return enemies.Length > 0; // Trả về true nếu có ít nhất một enemy trong phạm vi
    }

    // Tìm các enemy trong phạm vi skillRange
    private GameObject[] FindEnemiesInRange(float range)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> enemiesInRange = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= range)
            {
                enemiesInRange.Add(enemy); // Thêm enemy vào danh sách nếu trong phạm vi
            }
        }

        return enemiesInRange.ToArray(); // Trả về các enemy trong phạm vi
    }
}
