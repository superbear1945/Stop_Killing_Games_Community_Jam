using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;



public class ForcePointerScript : MonoBehaviour
{

    public GameObject forcePointerPosAdd;
    //力气值指针位置（增加）

    public GameObject forcePointerPosRed;
    //力气值指针位置 (减少)

    [Header("与力量条相关值")]
    public float _maxForce; //力气最大值
    public float _curForce; //力气当前值
    public float _lerdSpeed = 5; //力气条变化速率


    // Start is called before the first frame update
    void Start()
    {
        forcePointerPosAdd = transform.Find("Add").gameObject;
        forcePointerPosRed = transform.Find("Reduce").gameObject;

        if (forcePointerPosAdd == null)
        {
            Debug.LogError("增长力量条图片未获取");
        }
        if (forcePointerPosRed == null)
        {
            Debug.LogError("减少力量条图片未获取");
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
        if (GameManager._instance._isFishBite == false) //如果不处于鱼咬钩状态，就不需要更新力气指示条，力气指示条恒为百分之75
            _curForce = 50; //重置当前力气值

        if (_curForce > _maxForce / 2)
        {
            //如果当前力气值大于最大力气值的一半，则显示增加力量条指针，否则显示减少力量条指针
            forcePointerPosRed.gameObject.SetActive(false);
            forcePointerPosAdd.gameObject.SetActive(true);
            forcePointerPosAdd.GetComponent<Image>().fillAmount = Mathf.Lerp(a: forcePointerPosAdd.GetComponent<Image>().fillAmount, b: (_curForce - _maxForce / 2) / (_maxForce / 2), t: _lerdSpeed * Time.deltaTime);
        }
        else
        {
            //如果当前力气值小于等于最大力气值的一半，则显示减少力量条指针，否则显示增加力量条指针
            forcePointerPosRed.gameObject.SetActive(true);
            forcePointerPosAdd.gameObject.SetActive(false);
            forcePointerPosRed.GetComponent<Image>().fillAmount = Mathf.Lerp(a: forcePointerPosRed.GetComponent<Image>().fillAmount, b: (_maxForce / 2 - _curForce) / (_maxForce / 2), t: _lerdSpeed * Time.deltaTime);
        }
    }
}
