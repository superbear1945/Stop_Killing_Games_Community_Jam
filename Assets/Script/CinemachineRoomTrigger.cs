using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineRoomTrigger : MonoBehaviour
{
    Cinemachine.CinemachineVirtualCamera _virtualCamera;
    int _activePriority = 11; // 摄像机激活时的优先级
    int _inactivePriority = 10; // 摄像机非激活时的优先级

    void Awake()
    {
        _virtualCamera = GetComponentInParent<Cinemachine.CinemachineVirtualCamera>();
        if (_virtualCamera == null)
            Debug.LogError("没找到CinemachineVirtualCamera组件");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //玩家进入区域时，提升摄像机优先级
            _virtualCamera.Priority = _activePriority;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //玩家离开区域时，降低摄像机优先级
            _virtualCamera.Priority = _inactivePriority;
        }
    }

}
