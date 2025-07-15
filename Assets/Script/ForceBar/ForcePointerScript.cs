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

    // Start is called before the first frame update
    void Start()
    {
        forcePointerPos = GetComponent<Image>();
        if (forcePointerPos == null)
        {
            Debug.LogError("ForcePointer component is missing from forcePointer component!");
        }
        //初始化力气指示条位置

        //Bear: UIManager的正确用法如下，不需要想办法获取到UIManager实例，直接通过类名调用_instance即可
        //Bear: 不需要先定义一个UIManager属性，再在通过FindObjectByTag获取
        UIManager._instance.SetForce();//调用SetForce函数，使用当前力气值和最大力气值
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
