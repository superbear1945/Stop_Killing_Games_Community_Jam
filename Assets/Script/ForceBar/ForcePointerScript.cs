using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class ForcePointerScript : MonoBehaviour
{
    public Image forcePointerPos;
    //力气值指针位置

    public float _maxForce;
    public float _curForce;
    //力气最大值，当前值

    private float _lerdSpeed = 20;
    //力气条变化速率

    UIManager _manager;
    //调用UIManager脚本，获取力气值

    // Start is called before the first frame update
    void Start()
    {
        forcePointerPos = GetComponent<Image>();
        if (forcePointerPos == null)
        {
            Debug.LogError("ForcePointer component is missing from forcePointer component!");
        }
        //初始化力气指示条位置

        _manager = GameObject.Find("UIManager").GetComponent<UIManager>();
        if (_manager == null)
        {
            Debug.LogError("UIManager is missing from the scene!");
        }
        _manager.SetForce(75, 100);//调用SetForce函数，设置初始力气值和最大值
    }

    // Update is called once per frame
    void Update()
    {
        
        ChangePos();
    }

    void ChangePos()
    {
        forcePointerPos.fillAmount = Mathf.Lerp(a: forcePointerPos.fillAmount, b: _curForce / _maxForce, t: _lerdSpeed * Time.deltaTime);
    }

}
