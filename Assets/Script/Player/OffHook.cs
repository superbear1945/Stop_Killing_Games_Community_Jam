using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffHook : MonoBehaviour
{
    public void OnOffHook()
    {
        GameManager._instance._isFishBite = false; //设置鱼咬钩状态为false
        PlayerAnimController.Instance.SetTrigger("OffHook"); //设置收杆动画触发器
        UIManager._instance.ResetForce(); //重置力量值
    }
}
