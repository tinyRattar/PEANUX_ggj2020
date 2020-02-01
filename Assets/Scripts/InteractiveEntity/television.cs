using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Television : InteractiveEntity
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool OnInteract(KeyCode keyCode)
    {
        return false;
    }

    public override void AddRequiredTool(){
   // 坏掉的时候所需要的工具
        hammer_required++;
        screwer_required++;
    }

    // 纯虚函数，家具的最大耐久度，每个家具不一样
    public virtual void SetDurabilityMax(){
        durability_max = 30.0f;
    }
}
