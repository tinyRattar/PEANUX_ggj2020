using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : InteractiveEntity
{
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
        return; // 花无法回复，只能通过pee来回复durability
    }

    // 纯虚函数，家具的最大耐久度，每个家具不一样
    public override void SetDurabilityMax(){
        durability_max = 20.0f;
    }

    public virtual void OnCatInteract(CatAttackType type) {
        switch(type){
            case CatAttackType.bite:
                durability -= bite_damage;
                break;
            case CatAttackType.scratch:
                durability -= scratch_damage;
                break;
            case CatAttackType.pee:
                durability -= pee_damage;
                break;
            default:
                break;
        }
        if(durability <= 0){
            durability = 0;
            OnTotalDamage();
        }
        else{
            if(type == CatAttackType.pee){
                AddCleaningTool();
                durability+=5;
                if(durability>durability_max){
                  durability=durability_max;
                }
            }
            else{
                AddRequiredTool();
            }
        }
     }
}
