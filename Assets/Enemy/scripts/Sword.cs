using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 /*   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))  // Kiểm tra nếu đối tượng va chạm có tag "Enemy"
        {
            // Gọi hàm gây sát thương trên kẻ thù
            EnemyMove enemy = other.GetComponent<EnemyMove>();
            if (enemy != null)
            {
                float damage = Random.Range(5f, 10f);  // Giá trị sát thương cho kiếm
                enemy.UpdateHealth(enemy.HeathEnemy - damage);  // Giảm máu của kẻ thù
                Debug.Log("Gây sát thương cho kẻ thù: " + damage);
            }
        }
    }*/
}
