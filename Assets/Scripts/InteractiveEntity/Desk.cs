using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : InteractiveEntity
{
    [SerializeField] bool additionDecorateDamage = false;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   // 坏掉的时候所需要的工具
    public override void AddRequiredTool(){
        if(additionDecorateDamage)
        {
            float r = Random.Range(0, 2);
            if (r == 0)
                RandomChoiceDecorateTools();
            else
                RandomChoiceMechTools();
        }
        else
        {
            RandomChoiceMechTools();
        }
    }

    // 纯虚函数，家具的最大耐久度，每个家具不一样
    public override void SetDurabilityMax(){
        durability_max = 20.0f;
    }
}
