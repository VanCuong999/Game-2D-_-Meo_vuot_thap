using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletL1 : MonoBehaviour
{
    public Staft staft;
    public float Speed = 4.5f;
    private Vector3 direction;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * Speed;
        Destroy(gameObject, 3f);
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized; // Chỉ định hướng di chuyển cho viên đạn
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("EnemyGolem"))
        {
            GolemEnemy golemEnemy = other.GetComponent<GolemEnemy>();
            golemEnemy.TakeDangage(staft.sodamgagekhoidau);
            
        }
    }
}
