using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class FightFish : MonoBehaviour
{

    PlayerInput _playerInput;
    InputAction _fightFishAction;

    void Awake()
    {
        //获取PlayerInput组件
        _playerInput = GetComponent<PlayerInput>();
        if (_playerInput == null)
            Debug.LogError("未找到PlayerInput组件");

        //获取FightAction
        _fightFishAction = _playerInput.actions["Fightfish"];
        if (_fightFishAction == null)
            Debug.LogError("未找到Fightfish动作");
    }

    void Start()
    {
        //注册Fightfish动作的回调
        _fightFishAction.started += OnMouseLeftHold;
        _fightFishAction.canceled += OnMouseLeftRelease;
    }

    void OnDestroy()
    {
        //取消注册Fightfish动作的回调
        _fightFishAction.started -= OnMouseLeftHold;
        _fightFishAction.canceled -= OnMouseLeftRelease;
    }

    //Bear：按住鼠标左键时触发的回调函数
    void OnMouseLeftHold(InputAction.CallbackContext context)
    {
        UIManager._instance.OnMouseHold(); //通知UIManager进行力量增加
    }

    //Bear：松开鼠标左键时调用的回调函数
    void OnMouseLeftRelease(InputAction.CallbackContext context)
    {
        UIManager._instance.OnMouseLeftRelease(); //通知UIManager进行力量释放
    }
}
