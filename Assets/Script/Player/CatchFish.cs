using System;
using System.Collections;
using UnityEngine;

//鱼的类型枚举
public enum FishType
{
    _None,
    _Shark,
    _Bigfish,
    _Smallfish
}

//Bear: 协程可以理解为一种特殊的函数，它可以暂停执行并在未来某个时刻继续执行，非常适合处理时间延迟或等待事件的逻辑。
//咬钩检测系统
public class CatchFish : MonoBehaviour
{
    public static CatchFish Instance { get; private set; } // 单例模式，方便全局访问

    private Coroutine _detectionCoroutine; // 用于存储检测协程的引用，方便停止

    //咬钩事件，使用Action<FishType>委托，当有鱼咬钩时触发
    public event Action<FishType> OnFishBiting;

    private void Awake()
    {
        // 实现单例模式
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 保证在场景切换时不被销毁
        }
        else
        {
            Destroy(gameObject); // 如果已存在实例，则销毁当前对象
        }
    }

    //开始检测咬钩
    public void StartDetection()
    {
        Debug.Log("甩杆动作完成，准备开始检测咬钩...");
        // 如果已有检测协程在运行，先停止它
        if (_detectionCoroutine != null)
        {
            StopCoroutine(_detectionCoroutine);
        }
        // 启动新的检测协程
        _detectionCoroutine = StartCoroutine(PerformDetection());
    }

    //停止检测
    public void StopDetection()
    {
        // 如果检测协程正在运行，则停止它
        if (_detectionCoroutine != null)
        {
            StopCoroutine(_detectionCoroutine);
            _detectionCoroutine = null; // 清空引用
            Debug.Log("停止咬钩检测");
        }
    }

    //开始检测（协程）
    private IEnumerator PerformDetection()
    {
        // 等待1秒，模拟鱼饵落水后到开始有鱼的时间
        yield return new WaitForSeconds(1f); // 模拟准备时间
        Debug.Log("开始检测咬钩...");

        // 无限循环，直到有鱼上钩
        while (true)
        {
            // 每0.2秒检测一次
            yield return new WaitForSeconds(0.2f);
            //进行咬钩判定
            var result = DetermineFishBiting();
            // 如果判定有鱼咬钩（不是_None类型）
            if (result != FishType._None)
            {
                GameManager._instance._isFishBite = true; //设置鱼咬钩状态，通知游戏进入搏鱼阶段
                //触发咬钩事件，并传递鱼的类型
                OnFishBiting?.Invoke(result);
                break; // 跳出循环，停止检测
            }
        }
    }

    //判定是否有鱼咬钩
    private FishType DetermineFishBiting()
    {
        // 生成一个0到1之间的随机浮点数
        float randomValue = UnityEngine.Random.value;
        // 根据随机数和预设概率判断上钩的鱼的类型
        if (randomValue < 0.05)// 5% 概率是鲨鱼
        {
            Debug.Log("检测到有鲨鱼咬钩！");
            return FishType._Shark;
        }
        else if (randomValue < 0.15)// 10% 概率是大鱼
        {
            Debug.Log("检测到有大鱼咬钩！");
            return FishType._Bigfish;
        }
        else if (randomValue < 0.3)// 15% 概率是小鱼
        {
            Debug.Log("检测到有小鱼咬钩！");
            return FishType._Smallfish;
        }
        // 如果随机数不满足任何条件，则没有鱼咬钩
        Debug.Log("本次检测没有鱼咬钩");
        return FishType._None;
    }
}