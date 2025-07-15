using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager _instance; // 单例实例
    static public Player _currentPlayer; // 当前玩家的引用
    public bool _isFishBite = false; //用于判断鱼是否咬钩

    void Awake()
    {
        // 实现单例模式
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // 切换场景时不销毁该对象
        }
        else
        {
            Destroy(gameObject); // 如果实例已存在，则销毁当前对象
        }
    }

    // 注册玩家
    public void RegisterPlayer(Player player)
    {
        _currentPlayer = player;
    }
}
