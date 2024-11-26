using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotionSpeed;

    [SerializeField]
    private float _ScreenBoder;

    private Vector2 _MovementInput;
    private Rigidbody2D _rigidbody;
    private Vector2 _SmootheMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    private Camera _camera;

    private void Awake()
    {
       _rigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }
    private void FixedUpdate()
    {
        SetPlayvelocity();
        rotationInput();
    }

    private void SetPlayvelocity()
    {
        _SmootheMovementInput = Vector2.SmoothDamp(
                    _SmootheMovementInput,
                    _MovementInput,
                    ref _movementInputSmoothVelocity,
                    0.1f);
       _rigidbody.velocity = _SmootheMovementInput * speed;

        PreventPlayerGoingOffScreen();

    }
    private void PreventPlayerGoingOffScreen()
    {
        Vector2 ScreenPosition = _camera.WorldToScreenPoint(transform.position);
        if((ScreenPosition.x < _ScreenBoder && _rigidbody.velocity.x < 0) ||
            (ScreenPosition.x >_camera.pixelWidth - _ScreenBoder && _rigidbody.velocity.x > 0)) 
        {
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        }
        if ((ScreenPosition.y < _ScreenBoder && _rigidbody.velocity.x < 0) ||
            (ScreenPosition.y > _camera.pixelHeight - _ScreenBoder && _rigidbody.velocity.y > 0))
        {
            _rigidbody.velocity = new Vector2( _rigidbody.velocity.x,0);
        }
    }
    private void OnMove(InputValue inputValue)
    {
        _MovementInput = inputValue.Get<Vector2>();
    }
    private void rotationInput()
    {
        if (_MovementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _SmootheMovementInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotionSpeed * Time.deltaTime);
           
            _rigidbody.MoveRotation(rotation);
        }
    }
}
