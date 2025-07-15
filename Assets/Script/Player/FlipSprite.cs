using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    Rigidbody2D _rb2d;

    void Awake()
    {
        // 获取 Rigidbody2D 组件
        _rb2d = GetComponent<Rigidbody2D>();
        if (_rb2d == null)
            Debug.LogError("未找到Rigidbody2D组件");
    }

    void FixedUpdate()
    {
        if (_rb2d.velocity.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (_rb2d.velocity.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
