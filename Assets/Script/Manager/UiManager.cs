using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public event Action<float> OnPressureChange; // 压力变化事件
    
    public static UIManager _instance;
    
    //控制力量表的系数HeaderAttribute
    [Header("力量表设置")]
    public float _lerdSpeed = 2.0f; // 力量表增速度系数
    public float _dissipateSpeed = 2.0f; // 力量表衰减系数

    //Bear：在UIManager中，尽量使用Inspector拖拽的方式为属性赋值，比如下面的ForcePointerScript属性
    [SerializeField] ForcePointerScript _forcePointerScript; //通过在Inspector中拖拽的方式获取力量表指针
    [SerializeField] PressurePanelScript _pressurePanelScript; //通过在Inspector中拖拽的方式获取压力表指针
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); //Bear: 保持UIManager在场景切换时不被销毁
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if(_forcePointerScript == null)
            Debug.LogError("ForcePointerScript is not assigned in UIManager!");
        if(_pressurePanelScript == null)
            Debug.LogError("PressurePanelScript is not assigned in UIManager!");
    }


    //实现力量表增减量作用与压力表
    public float GetCurForce()
    {
        return _forcePointerScript._curForce;
    }

    //Bear: 用于在钓鱼动作结束后重置力量值
    public void ResetForce(float curForce = 50, float maxForce = 100)
    {
        _forcePointerScript._curForce = curForce;
        _forcePointerScript._maxForce = maxForce;
    }


    //Bear：不乘以Time.deltaTime的话会导致帧数不同的人力量条增减速度不一致
    //Bear：增加力量值的方法，会在FightFish中按住左键时被使用
    public void AddForce()
    {
        if(_forcePointerScript._curForce <  _forcePointerScript._maxForce)
        {
            _forcePointerScript._curForce += _forcePointerScript._lerdSpeed * Time.deltaTime*_lerdSpeed;
        }
    }

    //Bear：减少力量值的方法，会在FightFish中松开左键时被使用
    public void ReduceForce()
    {
        if (_forcePointerScript._curForce > 0)
        {
            _forcePointerScript._curForce -= _forcePointerScript._lerdSpeed * Time.deltaTime*_dissipateSpeed;
        }
        
    }

    //控制压力变化相关
    // 为UI中的压力值提供数值的接口
    public void ResetPressure()
    {
        _pressurePanelScript.ResetPressure(); // 重置压力值
    }


    // 触发压力变化事件
    public void TriggerPressureChange(float amount)
    {
        OnPressureChange?.Invoke(amount);
    }

    void GameQuit()
    {
        Application.Quit();
    }
}
