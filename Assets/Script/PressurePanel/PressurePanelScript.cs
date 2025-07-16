using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressurePanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private UnityEngine.UI.Image pressurePointer;
    public RectTransform rectTransform;

    public float _lerpSpeed = 0.1f; // 插值速度
    public float _maxPressure;
    public float _curPressure;
    private float _angle;
    void Start()
    {
        if (pressurePointer == null) 
        {
            Debug.LogError("未找到压力指针组件，请在Inspector中设置pressurePointer");
        }

        rectTransform = pressurePointer.rectTransform;
        if(rectTransform == null)
        {
            Debug.LogError("未找到压力指针rectTransform组件");
        }

        rectTransform.rotation = Quaternion.Euler(0, 0, 45);
        UIManager._instance.SetPressure();
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
                Debug.Log($"Angle={_angle}，压力值超过最大值，指针指向-90度");
            
            }
                
            else if(_curPressure <= 0)
            {
                Debug.Log($"Angle={_angle}，压力值为0或负数，指针指向90度");

            }
                
        }
        
        rectTransform.rotation = Quaternion.Euler(0, 0, _angle);
    }
}
