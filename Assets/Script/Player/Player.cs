using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Move _moveComponent;
    public static Player _instance;

    void Awake()
    {
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

    void Start()
    {
        _moveComponent = GetComponent<Move>();
        if (_moveComponent == null) //非空检查
            Debug.LogError("Move component is missing from Player GameObject.");
    }
}
