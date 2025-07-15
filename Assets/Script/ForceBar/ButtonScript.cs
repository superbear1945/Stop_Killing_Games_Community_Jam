using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//该脚本仅用于测试力量条功能，不参与正式游戏流程
public class ButtonScript : MonoBehaviour
{
    
    [SerializeField] public ForcePointerScript forcePointerScript;
    //创建ForcePointerScript对象，方便引用当前力量值进行测试
    private void Start()
    {
        forcePointerScript = GameObject.Find("ForcePointer").GetComponent<ForcePointerScript>();
        if (forcePointerScript == null)
        {
            Debug.LogError("Button:forcePointerScript is missing from ForcePointer component");
        }
    }
    public void AddForce()
    {
        if (forcePointerScript._curForce <= forcePointerScript._maxForce)
        {
            forcePointerScript._curForce += 1;
        }
    }
    public void ReduceForce()
    {
        if (forcePointerScript._curForce>=0)
        {
            forcePointerScript._curForce -= 1;
        }
    }
}
