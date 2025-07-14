using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("甩杆参数")]
    public float castForce = 10f;      // 甩杆力度
    public float shootDelay = 0.5f;//甩杆动作延迟（s）
    public GameObject yuEr;//鱼饵预制体
    private bool isShooting = false;//是否开始钓鱼了

    void Update()
    {
        //检测右键是否按下
        if (Input.GetMouseButtonDown(1) && !isShooting)
        {
            StartShoot();
        }
    }

    void StartShoot()
    {
        isShooting = true;
        Debug.Log("开始甩杆！");
        //甩杆动作
        //GetComponent<Animator>().SetTrigger("Cast");
        //生成鱼饵
        Invoke("SpawnBait", shootDelay);
    }
    void SpawnBait()
    {
        if (yuEr != null)
        {
            //生成鱼饵
            Vector2 spawnPos = (Vector2)transform.position + Vector2.right * 1f;//向右甩杆
            // 实例化鱼饵
            GameObject bait = Instantiate(yuEr, spawnPos, Quaternion.identity);
             // 添加 2D 物理力
            Rigidbody2D baitRb = bait.GetComponent<Rigidbody2D>();
            if (baitRb != null)
            {
                baitRb.AddForce(Vector2.right * castForce, ForceMode2D.Impulse); // 向右施力
            }
        }
        isShooting = false;
    }
}
