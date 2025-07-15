using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    void Start()
    {
        if (GameManager._instance != null)
        {
            GameManager._instance.RegisterPlayer(this); //注册当前玩家，在其它脚本中可以通过GameManager获取玩家
        }
    }
}
