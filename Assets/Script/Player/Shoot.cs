using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [Header("甩杆参数")]
    public float castForce = 10f;      // 甩杆力度
    public float shootDelay = 0.5f;//甩杆动作延迟（s）
    private bool _isShooting = false; //是否开始钓鱼了

    PlayerInput _playerInput;
    InputAction _shootAction;

    void Awake()
    {
        // 获取 PlayerInput 组件
        _playerInput = GetComponent<PlayerInput>();
        if (_playerInput == null)
        {
            Debug.LogError("未发现PlayerInput组件");
        }

        //获取甩杆Action
        _shootAction = _playerInput.actions["Shoot"];
        if (_shootAction == null)
        {
            Debug.LogError("未发现Shoot Action");
        }

        //绑定甩杆事件
        _shootAction.performed += StartShoot;
    }

    void OnDisable()
    {
        _shootAction.performed -= StartShoot;
    }

    void StartShoot(InputAction.CallbackContext context)
    {
        if (_isShooting) return; //防止不停甩杆鬼畜

        _isShooting = true;
        Debug.Log("开始甩杆！");
        //甩杆动作
        GetComponent<Animator>().SetTrigger("Shoot");
        //生成鱼
        SpawnBait();
    }

    void SpawnBait()
    {
        GameManager._instance._isFishing = true; //设置玩家正在钓鱼
        Vector3 playerDirection = new Vector3(transform.localScale.x, 0, 0); //获取玩家方向
        Vector2 spawnPos = transform.position + playerDirection * 1f;//向角色的右边甩杆
        CatchFish.Instance.StartDetection(); // 开始咬钩检测
        _isShooting = false; //重置甩杆状态
    }
}
