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

    [Header("与力量条相关值")]
    public float _maxForce; //力气最大值
    public float _curForce; //力气当前值
    public float _lerdSpeed = 5; //力气条变化速率
    

    // Start is called before the first frame update
    void Start()
    {
        forcePointerPos = GetComponent<Image>();
        if (forcePointerPos == null)
        {
            Debug.LogError("ForcePointer component is missing from forcePointer component!");
        }

        //初始化力气指示条位置
        //Bear: 不需要先定义一个UIManager属性，再在通过FindObjectByTag获取
        //Bear: UIManager的正确用法如下，不需要想办法获取到UIManager实例，直接通过类名调用_instance即可
        UIManager._instance.ResetForce();//调用ResetForce函数，初始化当前力气值和最大力气值
    }

    // Update is called once per frame
    void Update()
    {
        ChangePos();
    }

    void ChangePos()
    {
        if(GameManager._instance._isFishBite == false) //如果不处于鱼咬钩状态，就不需要更新力气指示条，力气指示条恒为百分之75
            _curForce = 50; //重置当前力气值
        forcePointerPos.fillAmount = Mathf.Lerp(a: forcePointerPos.fillAmount, b: _curForce / _maxForce, t: _lerdSpeed * Time.deltaTime);
    }

}
