using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class ForcePointerScript : MonoBehaviour
{
    public Image forcePointerPos;
    public GameObject forceBar;
    //力气值指针位置与力气条数据声明

    public float maxForce;
    public float curForce;
    //力气最大值，最小值，当前值，控制力气变化的数值需要在此处对接

    private float lerdSpeed = 20;
    //力气条变化速率

    // Start is called before the first frame update
    void Start()
    {
        forcePointerPos = GetComponent<Image>();
        forceBar = GetComponent<GameObject>();
        if (forcePointerPos == null)
        {
            Debug.LogError("ForcePointer component is missing from forcePointer component!");
        }
        if(forceBar == null)
        {
            Debug.LogError("ForceBar component is missing from forceBar component!");
        }
        maxForce = 100;
        curForce = 75;
        //数值初始化，实际使用中需要对接其他脚本获取当前力气值
    }

    // Update is called once per frame
    void Update()
    {
        ChangePos();
    }

    void ChangePos()
    {
        forcePointerPos.fillAmount = Mathf.Lerp(a: forcePointerPos.fillAmount, b: curForce / maxForce, t: lerdSpeed * Time.deltaTime);
    }

}
