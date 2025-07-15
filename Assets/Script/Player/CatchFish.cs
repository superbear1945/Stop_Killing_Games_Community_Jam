using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
namespace FishingGame
{
    //鱼的类型枚举
    public enum FishType {
        _None,
        _Shark,
        _Bigfish,
        _Smallfish
    }
    
    //咬钩事件参数
    public class FishBitingEventArgs : EventArgs
    {
        public FishType FishType { get; set; }
    }
    
    //咬钩检测系统
    public class BitingDetectionSystem
    {
        private readonly System.Random random = new System.Random();
        private CancellationTokenSource cancellationTokenSource;
        //咬钩事件
        public event EventHandler<FishBitingEventArgs> FishBiting;
        //开始检测咬钩
        public void StartDetection()
        {
            Console.WriteLine("甩杆动作完成，准备开始检测咬钩...");
            //wait 1s
            Thread.Sleep(1000);
            Console.WriteLine("开始检测咬钩...");

            cancellationTokenSource = new CancellationTokenSource();

            //使用线程池执行定时检测任务
            ThreadPool.QueueUserWorkItem(_ => PerformDetection(cancellationTokenSource.Token));
        }
        //stop detection
        public void StopDetection()
        {
            cancellationTokenSource?.Cancel();
            Console.WriteLine("停止咬钩检测");
        }
        //begin detection
        private void PerformDetection(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                //one detect/0.2s
                Thread.Sleep(200);
                //进行咬钩判定
                var result = DetermineFishBiting();
                if (result != FishType._None)
                {
                    //触发咬钩事件
                    OnFishBiting(new FishBitingEventArgs { FishType = result });
                    break;
                }
            }
        }
        //判定是否有鱼咬钩
        private FishType DetermineFishBiting()
        {
            double randomValue = random.NextDouble();
            if (randomValue < 0.05)//0.05 shark
            {
                Console.WriteLine("检测到有鱼咬钩！正在确定鱼的种类...");
                return FishType._Shark;
            }
            else if (randomValue < 0.15)//0.1 bigfish
            {
                Console.WriteLine("检测到有鱼咬钩！正在确定鱼的种类...");
                return FishType._Bigfish;
            }
            else if (randomValue < 0.3)//0.15 small fish
            {
                Console.WriteLine("检测到有鱼咬钩！正在确定鱼的种类...");
                return FishType._Smallfish;
            }
            Console.WriteLine("本次检测没有鱼咬钩");
            return FishType._None;
        }
        // 触发咬钩事件的方法
        protected virtual void OnFishBiting(FishBitingEventArgs e)
        {
            FishBiting?.Invoke(this, e);
        }
    }
}