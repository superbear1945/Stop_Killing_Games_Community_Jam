using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//璇ヨ剼鏈�涓昏�佺洰鐨勫湪浜庤В鑰︼紝灏嗕竴浜涘叏灞€鍙橀噺闆嗕腑绠＄悊
public class GameManager : MonoBehaviour
{
    static public GameManager _instance;
    static public Player _currentPlayer; //璁板綍褰撳墠鐜╁�讹紝鍙�浠ュ湪鍏跺畠鑴氭湰涓�琚�鑾峰彇

    void Awake()
    {
        //鍗曚緥妯″紡瀹炵幇
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

    public void RegisterPlayer(Player player) //娉ㄥ唽鐜╁�剁殑鍑芥暟
    {
        _currentPlayer = player;
    }
}
