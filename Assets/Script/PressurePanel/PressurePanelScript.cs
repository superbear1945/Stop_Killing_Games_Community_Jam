using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressurePanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    private UnityEngine.UI.Image pressurePointer;
    public RectTransform rectTransform;

    // Bear: _lerpSpeed 用于控制UI指针变化的平滑速度。数值越大，指针反应越快。
    public float _lerpSpeed = 0.5f; // 插值速度

    // Bear: _maxPressure 定义了压力的最大值。
    public float _maxPressure = 100;

    // Bear: _curPressure 是当前的、真实的压力目标值。这个值会立即变化。
    public float _curPressure = 50;

    // Bear: _displayPressure 是用于UI显示的压力值。它会通过插值计算平滑地趋近_curPressure，从而实现视觉上的平滑过渡效果。
    private float _displayPressure;
    private float _angle;
    void Awake()
    {
        pressurePointer = GetComponent<UnityEngine.UI.Image>();
        if (pressurePointer == null)
        {
            Debug.LogError("未找到压力指针组件，请在Inspector中设置pressurePointer");
        }

        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("未找到压力指针rectTransform组件");
        }
        // Bear: 初始化时，让显示的压力值等于当前的压力值，避免UI在开始时跳动。
        _displayPressure = _curPressure;
    }

    void Start()
    {
        // 确保UIManager实例存在后再订阅事件
        if (UIManager._instance != null)
        {
            UIManager._instance.OnPressureChange += UpdatePressure;
        }
    }

    void OnDestroy()
    {
        // 在对象销毁时取消订阅，防止内存泄漏
        if (UIManager._instance != null)
        {
            UIManager._instance.OnPressureChange -= UpdatePressure;
        }
    }

    // Bear: 这个方法由GameManager的OnPressureChange事件调用，用于更新当前的压力目标值。
    void UpdatePressure(float amount)
    {
        _curPressure += amount;
    }

    // Update is called once per frame
    void Update()
    {
        // Bear: 每一帧都调用PointerRotation来更新指针位置
        PointerRotation();
    }

    void PointerRotation()
    {
        // Bear: 使用Mathf.Clamp确保当前压力值不会超过设定的最大和最小范围（0到_maxPressure）。
        _curPressure = Mathf.Clamp(_curPressure, 0, _maxPressure);
        // Bear: 这是实现平滑过渡的核心。每一帧，我们都让_displayPressure向_curPressure靠近一点。
        // Bear: _lerpSpeed * Time.deltaTime确保了过渡速度在不同帧率下保持一致。
        _displayPressure = Mathf.Lerp(_displayPressure, _curPressure, _lerpSpeed * Time.deltaTime);

        // Bear: 根据平滑过渡后的_displayPressure来计算指针应该旋转的角度。
        // Bear: 罗盘UI上，90度代表0压力，-90度代表最大压力。
        _angle = Mathf.Lerp(90, -90, _displayPressure / _maxPressure);

        //旋转压力表指针
        rectTransform.rotation = Quaternion.Euler(0, 0, _angle);
    }

    public void ResetPressure()
    {
        _curPressure = 50; // 重置当前压力值
        _displayPressure = _curPressure; // 同时重置显示的压力值
    }
}
