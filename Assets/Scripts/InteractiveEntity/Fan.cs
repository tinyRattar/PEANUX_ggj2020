using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : InteractiveEntity
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   // 坏掉的时候所需要的工具
    public void AddRequiredTool(){
        RandomChoiceMechTools(); // 花无法回复，只能通过pee来回复durability
    }

    // 纯虚函数，家具的最大耐久度，每个家具不一样
    public virtual void SetDurabilityMax(){
        durability_max = 30.0f;
    }

}
