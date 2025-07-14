using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("甩杆参数")]
    public float _castForce = 10f;      // 甩杆力度
    public float _shootDelay = 0.5f;//甩杆动作延迟（s）
    public GameObject _fishingBait;//鱼饵预制体
    private bool isShooting = false;//是否开始钓鱼了

    void Update()
    {
        Console.WriteLine();
        
        //检测右键是否按下
        if (Input.GetMouseButtonDown(1) && !isShooting)
        {
            StartShoot();
        }
    }

    // Update is called once per frame
    void Update()
    void StartShoot()
    {
        isShooting = true;
        Debug.Log("开始甩杆！");
        //甩杆动作
        //GetComponent<Animator>().SetTrigger("Cast");
        //生成鱼饵
        Invoke("SpawnBait", _shootDelay);
    }
    void SpawnBait()
    {
        
        if (_fishingBait != null)
        {
            //生成鱼饵
            Vector2 spawnPos = (Vector2)transform.position + Vector2.right * 1f;//向右甩杆
            // 实例化鱼饵
            GameObject bait = Instantiate(_fishingBait, spawnPos, Quaternion.identity);
             // 添加 2D 物理力
            Rigidbody2D baitRb = bait.GetComponent<Rigidbody2D>();
            if (baitRb != null)
            {
                baitRb.AddForce(Vector2.right * _castForce, ForceMode2D.Impulse); // 向右施力
            }
        }
        isShooting = false;
    }
}