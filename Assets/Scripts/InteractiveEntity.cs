using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class InteractiveEntity : MonoBehaviour
{
    [SerializeField] float durability = 30;
    [SerializeField] float durability_max = 20;
    float bite_damage = 4;
    float scratch_damage = 4;
    float pee_damage = 4;
    float uniform_damage = 4;
    float repaired_ratio = 0.9;
    [SerializeField] int sewingKit_required = 0;
    [SerializeField] int hammer_required = 0;
    [SerializeField] int screwer_required = 0;
    [SerializeField] int spanner_required = 0;
    // Start is called before the first frame update
    virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual bool OnInteract(KeyCode keyCode) {
        // OnInteract(keyCode, person.current_tool)

     }

    public virtual bool OnInteract(KeyCode keyCode, ToolType current_tool) {
        if(Input.GetKeyDown(KeyCode.E)) {
            return OnRepair(current_tool);
        }

     }

    // 纯虚函数，每个家具不一样
    public virtual void AddRequiredTool(){
        
    }

    public virtual void OnCatInteract(CatAttackType type) {
        switch(type){
            case bite:
                durability -= bite_damage;
                break;
            case scratch:
                durability -= scratch_damage;
                break;
            case pee:
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
            AddRequiredTool();
        }
     }

    virtual void SetDurabilityMax(){

    }

    virtual void OnTotalDamage(){

    }

    virtual bool OnRepair(ToolType tool) { 
        if(isRequiredTool(tool)){
            
        }
        else{
            return false;
        }
        return false; 
    }

    bool isRequiredTool(ToolType tool){
        switch(tool){
            case sewingKit:
                if (sewingKit_required>0){
                    sewingKit_required--;
                    durability += uniform_damage*0.9;
                    return true;
                }
                break;
            case hammer:
                if (hammer_required>0){
                    hammer_required--;
                    durability += uniform_damage*0.9;
                    return true;
                }
                break;
            case screwer:
                if (screwer_required>0){
                    screwer_required--;
                    durability += uniform_damage*0.9;
                    return true;
                }
                break;
            case spanner:
                if (spanner_required>0){
                    spanner_required--;
                    durability += uniform_damage*0.9;
                    return true;
                }
                break;
            default:
                return false;
                break;
        }
        return false;
    }

    public virtual void DrawUI()
    {
        
    }



}
