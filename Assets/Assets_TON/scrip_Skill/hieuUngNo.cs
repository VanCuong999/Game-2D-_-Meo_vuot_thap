using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hieuUngNo : MonoBehaviour
{

    public float lifetime = 2f; // Th?i gian t?n t?i c?a hi?u ?ng n?

    void Start()
    {
        // H?y ??i t??ng sau th?i gian `lifetime`
        Destroy(gameObject, lifetime);
    }
}
