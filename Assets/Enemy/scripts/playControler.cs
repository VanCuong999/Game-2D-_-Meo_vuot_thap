using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class playControler : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }
    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField]
    private float _playerAwarenessDistance;
    private Transform _player;

    private void Awake()
    {
        // Tìm đối tượng Player trong scene
        _player = FindObjectOfType<Player>()?.transform;

        if (_player == null)
        {
            Debug.LogWarning("Player object not found! Ensure the Player is in the scene.");
        }
    }

    void Update()
    {
        // Kiểm tra _player có phải là null không trước khi sử dụng
        if (_player != null)
        {
            Vector2 enemyToPlayerVector = _player.position - transform.position;
            DirectionToPlayer = enemyToPlayerVector.normalized;

            if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance)
            {
                AwareOfPlayer = true;
            }
            else
            {
                AwareOfPlayer = false;
            }
        }
    }
}