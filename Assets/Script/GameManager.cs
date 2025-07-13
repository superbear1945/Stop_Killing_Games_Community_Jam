using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//该脚本主要目的在于解耦，将一些全局变量集中管理
public class GameManager : MonoBehaviour
{
    static public GameManager _instance;
    static public Player _currentPlayer; //记录当前玩家

    void Awake()
    {
        //单例模式实现
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterPlayer(Player player) //注册玩家的函数
    {
        _currentPlayer = player;
    }

    public Player GetPlayer() //获取当前玩家
    {
        return _currentPlayer;
    }
}
