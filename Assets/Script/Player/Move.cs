using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//可以用作参考，但是建议你们再自己重新写一遍移动逻辑
public class Move : MonoBehaviour
{
    PlayerInput _playerInput;
    InputAction _moveAction;
    Vector2 _inputDirection;
    Rigidbody2D _rb2d;

    [Header("移动设置")]
    [SerializeField] float _moveSpeed = 5f;

    void Awake()
    {
        //获取PlayerInput组件
        _playerInput = GetComponent<PlayerInput>();
        if (_playerInput == null)
            Debug.LogError("没找到PlayerInput组件");

        //获取移动输入
        _moveAction = _playerInput.actions["Move"];
        if (_moveAction == null)
            Debug.LogError("没找到移动输入");

        //获取Rigidbody2D组件
        _rb2d = GetComponent<Rigidbody2D>();
        if (_rb2d == null)
            Debug.LogError("没找到Rigidbody2D组件");
    }

    void Update()
    {
        _inputDirection = _moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        _rb2d.MovePosition(_rb2d.position + _inputDirection * _moveSpeed * Time.fixedDeltaTime);
    }
}
