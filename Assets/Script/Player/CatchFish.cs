using System;
using System.Collections;
using UnityEngine;

//Bear: 协程可以理解为一种特殊的函数，它可以暂停执行并在未来某个时刻继续执行，非常适合处理时间延迟或等待事件的逻辑。
//咬钩检测系统
public class CatchFish : MonoBehaviour
{
    public static CatchFish Instance { get; private set; } // 单例模式，方便全局访问

    private Coroutine _detectionCoroutine; // 用于存储检测协程的引用，方便停止

    //咬钩事件，使用Action<FishType>委托，当有鱼咬钩时触发
    public event Action<GameManager.FishType> OnFishBiting;

    //未咬钩事件
    public event Action OnOffHook;

    [Header("鱼类预制体")]
    public GameObject sharkPrefab; // 鲨鱼预制体
    public GameObject bigfishPrefab; // 大鱼预制体
    public GameObject smallfishPrefab; // 小鱼预制体

    [Header("生成设置")]
    public float spawnDistance = 5f; // 鱼与角色的距离

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
        }
    }

    //开始检测（协程）
    private IEnumerator PerformDetection()
    {
        // 等待1秒，模拟鱼饵落水后到开始有鱼的时间
        yield return new WaitForSeconds(1f); // 模拟准备时间
        Debug.Log("开始检测咬钩...");

        //进行咬钩判定
        var result = DetermineFishBiting();
        // 如果判定有鱼咬钩（不是_None类型）
        if (result != GameManager.FishType._None)
        {
            GameManager._instance._isFishBite = true; //设置鱼咬钩状态，通知游戏进入搏鱼阶段
            SpawnFish(result); //生成鱼
            //触发咬钩事件，并传递鱼的类型
            OnFishBiting?.Invoke(result);
        }
        else //如果没有鱼咬钩
        {
            StopDetection(); //停止检测
            OnOffHook?.Invoke(); //调用脱钩事件
        }
    }

    //判定是否有鱼咬钩
    private GameManager.FishType DetermineFishBiting()
    {
        // 生成一个0到1之间的随机浮点数
        //float randomValue = UnityEngine.Random.value;
        float randomValue = 0.2f; // 为了测试方便，始终钓小鱼，正式版记得将上一行的随机值取消注释
        Debug.Log("随机值：" + randomValue);
        // 根据随机数和预设概率判断上钩的鱼的类型
        if (randomValue < 0.05)// 5% 概率是鲨鱼
        {
            Debug.Log("检测到有鲨鱼咬钩！");
            return GameManager.FishType._Shark;
        }
        else if (randomValue < 0.15)// 10% 概率是大鱼
        {
            Debug.Log("检测到有大鱼咬钩！");
            return GameManager.FishType._Bigfish;
        }
        else if (randomValue < 0.3)// 15% 概率是小鱼
        {
            Debug.Log("检测到有小鱼咬钩！");
            return GameManager.FishType._Smallfish;
        }
        // 如果随机数不满足任何条件，则没有鱼咬钩
        Debug.Log("本次检测没有鱼咬钩");
        return GameManager.FishType._None;
    }

    void SpawnFish(GameManager.FishType fishType)
    {
        // 检查当前玩家是否存在
        if (GameManager._currentPlayer == null)
        {
            Debug.LogError("当前玩家不存在，无法生成鱼");
            return;
        }

        // 根据鱼的类型选择对应的预制体
        GameObject fishPrefab = null;
        switch (fishType)
        {
            case GameManager.FishType._Shark:
                fishPrefab = sharkPrefab;
                break;
            case GameManager.FishType._Bigfish:
                fishPrefab = bigfishPrefab;
                break;
            case GameManager.FishType._Smallfish:
                fishPrefab = smallfishPrefab;
                break;
            case GameManager.FishType._None:
                Debug.LogWarning("尝试生成类型为None的鱼");
                return;
            default:
                Debug.LogError("未知的鱼类型：" + fishType);
                return;
        }

        // 检查预制体是否存在
        if (fishPrefab == null)
        {
            Debug.LogError($"未设置{fishType}类型的鱼预制体");
            return;
        }

        // 计算生成位置：根据角色朝向在玩家前方生成
        Vector3 spawnPosition = CalculateSpawnPosition();

        // 生成鱼
        GameObject fish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
        GameManager._instance._currentFightingFishType = fishType; // 更新当前搏斗的鱼的类型

        // 设置鱼的类型
        FishRandomPressure fishPressure = fish.GetComponent<FishRandomPressure>();
        if (fishPressure != null)
        {
            fishPressure.fishType = fishType;
        }
        else
        {
            Debug.LogWarning($"在生成的鱼上没有找到 {nameof(FishRandomPressure)} 组件");
        }

        Debug.Log($"在位置 {spawnPosition} 生成了 {fishType} 类型的鱼");
    }

    // 计算鱼的生成位置（在玩家前方）
    private Vector3 CalculateSpawnPosition()
    {
        // 获取玩家当前朝向（通过localScale判断）
        Transform playerTransform = GameManager._currentPlayer.transform;
        float direction = playerTransform.localScale.x > 0 ? 1f : -1f;

        // 获取玩家的Collider2D组件
        Collider2D playerCollider = playerTransform.GetComponent<Collider2D>();
        
        // 计算玩家底部位置
        Vector3 basePosition = playerTransform.position;
        basePosition.y -= playerCollider.bounds.extents.y;

        // 在玩家底部前方指定距离处生成鱼
        Vector3 spawnPosition = basePosition + new Vector3(direction * spawnDistance, 0, 0);

        return spawnPosition;
    }
}
