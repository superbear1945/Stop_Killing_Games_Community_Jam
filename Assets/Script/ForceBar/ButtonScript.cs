using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//该脚本仅用于测试力量条功能，不参与正式游戏流程
public class ButtonScript : MonoBehaviour
{
    
    public ForcePointerScript forcePointerScript;
    //创建ForcePointerScript对象，方便引用当前力量值进行测试
    private void Start()
    {
        forcePointerScript = GetComponent<ForcePointerScript>();
        if (forcePointerScript == null)
        {
            Debug.LogError("forcePointerScript is missing from ForcePointer component");
        }
    }
    public void AddForce()
    {
        if (forcePointerScript.curForce <= forcePointerScript.maxForce)
        {
            forcePointerScript.curForce += 1;
        }
    }
    public void ReduceForce()
    {
        if (forcePointerScript.curForce>=0)
        {
            forcePointerScript.curForce -= 1;
        }
    }
}
