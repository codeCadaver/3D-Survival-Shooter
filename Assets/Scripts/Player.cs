using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    private float _speed = 5f;
    [SerializeField]
    private GameObject _bullet;
    private PlayerControls _controls;

    private Vector3 _moveDirection;

    private void Awake()
    {
        _controls = new PlayerControls();
        _controls.Player.Fire.performed += OnFire;
        _controls.Player.Move.performed += OnMove;
        _controls.Player.Move.canceled += OnMove;
    }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        _moveDirection = new Vector3(input.x, _moveDirection.y, input.y);
    }

    private void OnFire(InputAction.CallbackContext ctx)
    {
        Debug.Log("Shot Fired!!!");
        GameObject bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
    }

    private void FixedUpdate()
    {
        _controller.Move(_moveDirection * (_speed * Time.deltaTime));
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
}
