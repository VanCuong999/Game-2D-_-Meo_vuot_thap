using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class condoi : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    [SerializeField]
    private float _rotationSpeed;

    [SerializeField]
    private float _ScreenBoder;

    private Animator anim;
    public int health = 100;
    private Rigidbody2D _rigidbody;

    private playControler _playControler;
    private Vector2 _targetDirection;
    private float _changDirectionCoolDown;
    private Camera _camera;

    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playControler = GetComponent<playControler>();
        anim = GetComponent<Animator>();
        _targetDirection = transform.up;
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void HandleRamdom()
    {
        _changDirectionCoolDown -= Time.deltaTime;
        if (_changDirectionCoolDown <= 0)
        {
            // Thay đổi hướng ngẫu nhiên theo chiều ngang (trục X)
            float angleChange = Random.Range(-90f, 90f); // Vẫn giữ ngẫu nhiên thay đổi góc
            Quaternion rotation = Quaternion.AngleAxis(angleChange, Vector3.forward);  // Quay theo trục Z
            _targetDirection = rotation * _targetDirection;

            _changDirectionCoolDown = Random.Range(1f, 5f);
        }
    }

    private void HandlePlayer()
    {
        if (_playControler.AwareOfPlayer)
        {
            // Nếu Player trong phạm vi tấn công, di chuyển theo hướng của Player
            _targetDirection = _playControler.DirectionToPlayer;
        }
    }
    private void HandleEnemy()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);
        if ((screenPosition.x < _ScreenBoder && _targetDirection.x < 0) ||
            (screenPosition.x > _camera.pixelWidth - _ScreenBoder && _targetDirection.x > 0))
        {
            // Nếu Enemy ra khỏi màn hình, quay lại
            _targetDirection = new Vector2(-_targetDirection.x, _targetDirection.y);
        }
        if ((screenPosition.y < _ScreenBoder && _targetDirection.y < 0) ||
            (screenPosition.y > _camera.pixelHeight - _ScreenBoder && _targetDirection.y > 0))
        {
            // Điều chỉnh khi ra khỏi biên giới dọc
            _targetDirection = new Vector2(_targetDirection.x, -_targetDirection.y);
        }
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        Setvelocity();
    }
    private void UpdateTargetDirection()
    {
        HandleRamdom();
        HandlePlayer();
        HandleEnemy();


    }
    private void RotateTowardsTarget()
    {
        // Quay Enemy theo hướng di chuyển mới
        if (_targetDirection.x != 0 || _targetDirection.y != 0) // Kiểm tra nếu có hướng di chuyển
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, _targetDirection);  // Quay theo trục Z
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            _rigidbody.SetRotation(rotation);
        }
    }
    private void Setvelocity()
    {
        // Di chuyển theo hướng đã xác định, chỉ di chuyển theo trục ngang hoặc dọc
        _rigidbody.velocity = new Vector2(_targetDirection.x, _targetDirection.y) * Speed; // Chỉ di chuyển theo chiều ngang hoặc dọc
    }
    public void TakeDamage()
    {
        Debug.Log(gameObject.name + " is taking damage!");
        if (gameObject != null)
        {
           
        }

        health -= 50;

        if (health <= 0)
        {
            KichHoatDieEnemy();

            Exp.Intance.TakeExp(Exp.Intance.exp);
            expPlayer.Intancs.GainExperience(10);
        }
    }
    public void KichHoatDieEnemy()
    {
        anim.SetTrigger("Ondied");
    }
    void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HeathPlayer.Intance.TakeHeath(10);
        }
    }

}
