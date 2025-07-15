using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    Animator _animator;
    static public PlayerAnimController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _animator = GetComponent<Animator>();
        if (_animator == null)
            Debug.LogError("未找到Animator组件");
    }

    //用于设置各种动画触发器
    public void SetTrigger(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }
}
