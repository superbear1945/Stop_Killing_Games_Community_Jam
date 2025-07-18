using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressurePanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    private UnityEngine.UI.Image pressurePointer;
    public RectTransform rectTransform;

    public float _lerpSpeed = 0.1f; // 插值速度
    public float _maxPressure = 100;
    public float _curPressure = 75;
    private float _angle;
    void Awake()
    {
        pressurePointer = GetComponent<UnityEngine.UI.Image>();
        if (pressurePointer == null)
        {
            Debug.LogError("未找到压力指针组件，请在Inspector中设置pressurePointer");
        }

        rectTransform = GetComponent<RectTransform>();
        if(rectTransform == null)
        {
            Debug.LogError("未找到压力指针rectTransform组件");
        }
    }

    void Start()
    {
        // 确保GameManager实例存在后再订阅事件
        if (GameManager._instance != null)
        {
            GameManager._instance.OnPressureChange += UpdatePressure;
        }
    }

    void OnDestroy()
    {
        // 在对象销毁时取消订阅，防止内存泄漏
        if (GameManager._instance != null)
        {
            GameManager._instance.OnPressureChange -= UpdatePressure;
        }
    }

    // 更新压力值
    void UpdatePressure(float amount)
    {
        _curPressure += amount;
    }

    // Update is called once per frame
    void Update()
    {
        PointerRotation();
    }

    void PointerRotation()
    {
        // 使用Mathf.Lerp来平滑过渡角度
        if (_maxPressure > 0 && _curPressure > 0 && _curPressure < _maxPressure)
        {
            _angle = Mathf.Lerp(90, -90, _curPressure / _maxPressure);
            Debug.Log($"Angle={_angle}");
        }
        else
        {
            if (_curPressure >= _maxPressure)
            {
                _angle = -90; // 超过最大值时指向-90度
                _curPressure = _maxPressure; // 确保压力值不超过最大

            }

            else if (_curPressure <= 0)
            {
                _angle = 90; // 小于等于0时指向90度
                _curPressure = 0; // 确保压力值不小于0
            }

        }

        //旋转压力表指针
        rectTransform.rotation = Quaternion.Euler(0, 0, _angle);
    }
}
