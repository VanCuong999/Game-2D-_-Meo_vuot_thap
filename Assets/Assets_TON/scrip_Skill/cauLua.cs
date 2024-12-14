using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cauLua : MonoBehaviour
{
    public GameObject fireballPrefab; // Prefab qu? c?u l?a
    public Transform spawnPoint;      // V? tr� t?o qu? c?u l?a
    public float speed = 5f;          // T?c ?? qu? c?u l?a
    public float skillRange = 10f;    // B�n k�nh ph?m vi chi�u
    public LayerMask enemyLayer;      // L?p c?a k? th� ?? ph�t hi?n

    void Update()
    {
        // Ki?m tra khi nh?n ph�m "T" v� c� �t nh?t m?t enemy trong ph?m vi
        if (Input.GetKeyDown(KeyCode.T) && IsEnemyInRange())
        {
            CastFireballSkill(); // K�ch ho?t chi�u khi nh?n ph�m "T"
        }
    }

    public void CastFireballSkill()
    {
        // L?y danh s�ch c�c enemy trong ph?m vi
        GameObject[] targets = FindEnemiesInRange(skillRange);

        // N?u c� �t nh?t m?t k? th� trong ph?m vi
        if (targets.Length > 0)
        {
            // Ch?n k? th� g?n nh?t
            GameObject target = targets[0];

            // Quay spawnPoint v? h??ng c?a k? th�
            Vector3 direction = (target.transform.position - spawnPoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            spawnPoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // T?o m?t qu? c?u l?a v� nh?m v�o m?c ti�u
            GameObject fireball = Instantiate(fireballPrefab, spawnPoint.position, spawnPoint.rotation);
            fireball.GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }

    // Ki?m tra xem c� �t nh?t m?t enemy n�o trong ph?m vi chi�u kh�ng v� thu?c l?p enemyLayer
    private bool IsEnemyInRange()
    {
        GameObject[] enemies = FindEnemiesInRange(skillRange);
        return enemies.Length > 0; // Tr? v? true n?u c� �t nh?t m?t enemy trong ph?m vi
    }

    // T�m c�c enemy trong ph?m vi skillRange v� thu?c l?p enemyLayer
    private GameObject[] FindEnemiesInRange(float range)
    {
        // T�m t?t c? c�c k? th� v?i tag "Enemy" v� "EnemyGolem"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] golems = GameObject.FindGameObjectsWithTag("EnemyGolem");

        // K?t h?p hai m?ng l?i
        List<GameObject> enemiesInRange = new List<GameObject>();
        enemiesInRange.AddRange(enemies);
        enemiesInRange.AddRange(golems);

        // Lo?i b? c�c k? th� kh�ng trong ph?m vi v� kh�ng thu?c l?p enemyLayer
        List<GameObject> enemiesInRangeFiltered = new List<GameObject>();

        foreach (GameObject enemy in enemiesInRange)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= range &&
                ((1 << enemy.layer) & enemyLayer) != 0)
            {
                enemiesInRangeFiltered.Add(enemy); // Th�m enemy v�o danh s�ch n?u trong ph?m vi v� thu?c l?p
            }
        }

        return enemiesInRangeFiltered.ToArray(); // Tr? v? c�c enemy trong ph?m vi
    }

}
