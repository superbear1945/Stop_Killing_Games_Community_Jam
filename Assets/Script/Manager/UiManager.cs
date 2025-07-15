using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    public static UIManager _instance;
    //Bear：在UIManager中，尽量使用Inspector拖拽的方式为属性赋值，比如下面的ForcePointerScript属性
    [SerializeField] ForcePointerScript _forcePointerScript; //通过在Inspector中拖拽的方式获取力量表指针
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
    }


    //Bear: 为UI中的两个力量值提供数值的接口
    public void SetForce(float curForce = 75, float maxForce = 100)
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
            _forcePointerScript._curForce += _forcePointerScript._lerdSpeed * Time.deltaTime;
        }
    }

    //Bear：减少力量值的方法，会在FightFish中松开左键时被使用
    public void ReduceForce()
    {
        if (_forcePointerScript._curForce > 0)
        {
            _forcePointerScript._curForce -= _forcePointerScript._lerdSpeed * Time.deltaTime;
        }
        
    }

    void GameQuit()
    {
        Application.Quit();
    }
}
