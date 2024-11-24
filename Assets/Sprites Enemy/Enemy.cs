using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    [SerializeField]
    private float _rotationSpeed;

    [SerializeField]
    private float _ScreenBoder;


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
        if (_changDirectionCoolDown <= 0) {
            float anlechange = Random.Range(-90f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(anlechange, transform.forward);
            _targetDirection = rotation * _targetDirection;

            _changDirectionCoolDown = Random.Range(1f, 5f);
        }
    }
    private void HandlePlayer()
    {
        if (_playControler.AwareOfPlayer)
        {
            _targetDirection = _playControler.DirectionToPlayer;
        }
    }
    private void HandleEnemy()
    {
        Vector2 ScreenPosition = _camera.WorldToScreenPoint(transform.position);
        if ((ScreenPosition.x < _ScreenBoder && _targetDirection.x < 0) ||
            (ScreenPosition.x > _camera.pixelWidth - _ScreenBoder && _targetDirection.x > 0))
        {
            _targetDirection = new Vector2( - _targetDirection.x,_targetDirection.y);
        }
        if ((ScreenPosition.y < _ScreenBoder && _targetDirection.y < 0) ||
            (ScreenPosition.y > _camera.pixelHeight - _ScreenBoder && _targetDirection.y > 0))
        {
            _targetDirection = new Vector2( _targetDirection.x, - _targetDirection.y);
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
       
        Quaternion TargetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, _rotationSpeed * Time.deltaTime);
        _rigidbody.SetRotation(rotation);
    }
    private void Setvelocity()
    {
        
            _rigidbody.velocity = transform.up * Speed;
        
    }
    public void TakeDamage()
    {
        Debug.Log(gameObject.name + " is taking damage!");
        health -= 50;

        if (health <= 0)
        {
            Die();
        }
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
            HeathPlayer playerHealth = collision.gameObject.GetComponent<HeathPlayer>();
            if (playerHealth != null)
            {
                playerHealth.TakeHeath(10);
            }
        }
    }


}
