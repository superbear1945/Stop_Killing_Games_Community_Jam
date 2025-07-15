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

    public void OnMouseHold()
    {
        Debug.Log("力量增加");
    }

    public void OnMouseLeftRelease()
    {
        Debug.Log("力量释放");
    }

    void GameQuit()
    {
        Application.Quit();
    }
}
