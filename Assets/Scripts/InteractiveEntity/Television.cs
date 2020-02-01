using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Television : InteractiveEntity
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

    void RandomChoiceTools(){
        // 暂时没有用到，改用基类的RandomChoiceMechTools
        int rand_choice = Random.Range(0,2);
        if (rand_choice == 0){
            hammer_required++;
        }
        else{
            screwer_required++;
        }
    }

   // 坏掉的时候所需要的工具
    public void AddRequiredTool(){
        RandomChoiceMechTools();
    }

    // 纯虚函数，家具的最大耐久度，每个家具不一样
    public virtual void SetDurabilityMax(){
        durability_max = 30.0f;
    }
}
