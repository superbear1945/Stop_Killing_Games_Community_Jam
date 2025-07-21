using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffHook : MonoBehaviour
{
    void Start()
    {
        CatchFish.Instance.OnOffHook += OnOffHook;
    }

    void OnDisable()
    {
        CatchFish.Instance.OnOffHook -= OnOffHook;
    }

    public void OnOffHook()
    {
        CatchFish.Instance.StopDetection(); //停止咬钩检测
        GameManager._instance._isFishBite = false; //设置鱼咬钩状态为false
        GameManager._instance._isFishing = false; //设置玩家钓鱼状态为false
        PlayerAnimController.Instance.SetTrigger("OffHook"); //设置收杆动画触发器
        UIManager._instance.ResetForce(); //重置力量值
        UIManager._instance.ResetPressure(); //重置压力值
    }
}
