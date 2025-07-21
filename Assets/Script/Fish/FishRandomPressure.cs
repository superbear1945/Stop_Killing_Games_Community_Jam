using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishRandomPressure : MonoBehaviour
{
    public GameManager.FishType fishType; // 鱼的类型
    float _updateInterval = 2.0f; // 更新间隔

    private float _range; // 压力随机范围

    void Start()
    {
        // 根据鱼的类型设置压力范围
        switch (fishType)
        {
            case GameManager.FishType._Shark:
                _range = 25f;
                break;
            case GameManager.FishType._Bigfish:
                _range = 21f;
                break;
            case GameManager.FishType._Smallfish:
                _range = 18f;
                break;
            default:
                _range = 0f;
                break;
        }

        // 开始定时产生压力
        StartCoroutine(GeneratePressure());
    }

    private IEnumerator GeneratePressure()
    {
        while (true)
        {
            _updateInterval = Random.Range(0.5f, 1.5f); // 随机更新间隔

            // 等待指定的时间间隔
            yield return new WaitForSeconds(_updateInterval);
            
            // 在(-_range, _range)之间生成一个随机值
            float pressureChange = Random.Range(-_range, _range);

            // 通过UIManager触发压力变化事件
            if (UIManager._instance != null)
            {
                UIManager._instance.TriggerPressureChange(pressureChange);
            }
        }
    }
}
