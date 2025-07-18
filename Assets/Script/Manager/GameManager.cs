using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //鱼的类型枚举
    public enum FishType
    {
        _None,
        _Shark,
        _Bigfish,
        _Smallfish
    }

    static public GameManager _instance; // 单例实例
    static public Player _currentPlayer; // 当前玩家的引用

     //用于判断鱼是否咬钩，因为很多地方可能都会用到这个判断，所以放入GameManager中供全局使用
    public bool _isFishBite = false;
    public bool _isFishing = false; // 是否正在钓鱼

    public GameObject _currentBait; // 当前鱼饵的引用

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
