using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cauLua : MonoBehaviour
{
    public GameObject fireballPrefab; // Prefab qu? c?u l?a
    public Transform spawnPoint;      // V? trí t?o qu? c?u l?a
    public float speed = 5f;          // T?c ?? qu? c?u l?a
    public float skillRange = 10f;    // Bán kính ph?m vi chiêu
    public LayerMask enemyLayer;      // L?p c?a k? thù ?? phát hi?n

    void Update()
    {
        // Ki?m tra khi nh?n phím "T" và có ít nh?t m?t enemy trong ph?m vi
        if (Input.GetKeyDown(KeyCode.T) && IsEnemyInRange())
        {
            CastFireballSkill(); // Kích ho?t chiêu khi nh?n phím "T"
        }
    }

    public void CastFireballSkill()
    {
        // L?y danh sách các enemy trong ph?m vi
        GameObject[] targets = FindEnemiesInRange(skillRange);

        // N?u có ít nh?t m?t k? thù trong ph?m vi
        if (targets.Length > 0)
        {
            // Ch?n k? thù g?n nh?t
            GameObject target = targets[0];

            // Quay spawnPoint v? h??ng c?a k? thù
            Vector3 direction = (target.transform.position - spawnPoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            spawnPoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // T?o m?t qu? c?u l?a và nh?m vào m?c tiêu
            GameObject fireball = Instantiate(fireballPrefab, spawnPoint.position, spawnPoint.rotation);
            fireball.GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }

    // Ki?m tra xem có ít nh?t m?t enemy nào trong ph?m vi chiêu không và thu?c l?p enemyLayer
    private bool IsEnemyInRange()
    {
        GameObject[] enemies = FindEnemiesInRange(skillRange);
        return enemies.Length > 0; // Tr? v? true n?u có ít nh?t m?t enemy trong ph?m vi
    }

    // Tìm các enemy trong ph?m vi skillRange và thu?c l?p enemyLayer
    private GameObject[] FindEnemiesInRange(float range)
    {
        // Tìm t?t c? các k? thù v?i tag "Enemy" và "EnemyGolem"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] golems = GameObject.FindGameObjectsWithTag("EnemyGolem");

        // K?t h?p hai m?ng l?i
        List<GameObject> enemiesInRange = new List<GameObject>();
        enemiesInRange.AddRange(enemies);
        enemiesInRange.AddRange(golems);

        // Lo?i b? các k? thù không trong ph?m vi và không thu?c l?p enemyLayer
        List<GameObject> enemiesInRangeFiltered = new List<GameObject>();

        foreach (GameObject enemy in enemiesInRange)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= range &&
                ((1 << enemy.layer) & enemyLayer) != 0)
            {
                enemiesInRangeFiltered.Add(enemy); // Thêm enemy vào danh sách n?u trong ph?m vi và thu?c l?p
            }
        }

        return enemiesInRangeFiltered.ToArray(); // Tr? v? các enemy trong ph?m vi
    }

}
