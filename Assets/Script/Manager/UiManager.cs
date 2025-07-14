using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] public ForcePointerScript forcePointerScript;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        forcePointerScript = GameObject.Find("ForcePointer").GetComponent<ForcePointerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //单例模型
    void GameStart()
    {

    }
    //切换场景，实现游戏开始，等待场景中

    public void SetForce(float curForce = 75,float maxForce = 100)
    {
        forcePointerScript._curForce = curForce;
        forcePointerScript._maxForce = maxForce;
    }
    //操作力气最大值，当前值的函数

    void GameQuit()
    {
        Application.Quit();
    }
}
