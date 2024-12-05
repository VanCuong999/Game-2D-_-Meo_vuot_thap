using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public float lifetime = 0.1f; // Thời gian tồn tại của vụ nổ

    void Start()
    {
        Destroy(gameObject, lifetime); // Tự hủy sau thời gian lifetime
    }
}
