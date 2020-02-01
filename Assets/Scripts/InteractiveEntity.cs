using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class InteractiveEntity : MonoBehaviour
{
    [SerializeField] public float durability = 30;
    [SerializeField] public float durability_max = 20;
    protected float bite_damage = 4;
    protected float scratch_damage = 4;
    protected float pee_damage = 4;
    protected float uniform_damage = 4;
    float repaired_ratio = 0.9f;
    [SerializeField] public int sewingKit_required = 0;
    [SerializeField] public int hammer_required = 0;
    [SerializeField] public int screwer_required = 0;
    [SerializeField] public int spanner_required = 0;
    [SerializeField] public int washKit_required = 0;
    [SerializeField] public int band_required = 0; //6
    [SerializeField] bool canAttack = true;
    public bool AttackCheck() { return canAttack; }

    [SerializeField] List<GameObject> listBubbles;
    // Start is called before the first frame update
    public void Init()
    {
        SetDurabilityMax();
        DrawUI();
    }

    public virtual bool OnInteract(KeyCode keyCode) {
        DrawUI();
        return OnInteract(keyCode, PlayerCTRL.instance.cur_tool);
    }

    public virtual bool OnInteract(KeyCode keyCode, ToolType current_tool) {
        if (keyCode == KeyCode.E) {
            return OnRepair(current_tool);
        }
        return false;
    }

    
    public virtual void AddRequiredTool(){
    // 坏掉的时候所需要的工具
    // 纯虚函数，每个家具不一样
        
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
            }
            else{
                AddRequiredTool();
            }
        }
        DrawUI();
    }

    // 纯虚函数，家具的最大耐久度，每个家具不一样
    public virtual void SetDurabilityMax(){

    }

    public void AddCleaningTool(){
        washKit_required++;
    }

    // 在完全被破坏的状态
    public virtual void OnTotalDamage(){

    }

    public virtual bool OnRepair(ToolType tool) {
        if (isRequiredTool(tool)){
            DrawUI();
            return true;
        }
        return false;
    }

    public bool isRequiredTool(ToolType tool){
        switch(tool){
            case ToolType.sewingKit:
                if (sewingKit_required>0){
                    sewingKit_required--;
                    durability += uniform_damage*0.9f;
                    PlayerCTRL.instance.sewingKit_num--;
                    PlayerCTRL.instance.cur_tool = ToolType.empty;
                    return true;
                }
                break;
            case ToolType.hammer:
                if (hammer_required>0){
                    hammer_required--;
                    durability += uniform_damage*0.9f;
                    PlayerCTRL.instance.hammer_num--;
                    PlayerCTRL.instance.cur_tool = ToolType.empty;
                    return true;
                }
                break;
            case ToolType.screwer:
                if (screwer_required>0){
                    screwer_required--;
                    durability += uniform_damage*0.9f;
                    PlayerCTRL.instance.screwer_num--;
                    PlayerCTRL.instance.cur_tool = ToolType.empty;
                    return true;
                }
                break;
            case ToolType.spanner:
                if (spanner_required>0){
                    spanner_required--;
                    durability += uniform_damage*0.9f;
                    PlayerCTRL.instance.spanner_num--;
                    PlayerCTRL.instance.cur_tool = ToolType.empty;
                    return true;
                }
                break;
            case ToolType.washKit:
                if (washKit_required > 0)
                {
                    washKit_required--;
                    durability += uniform_damage * 0.9f;
                    PlayerCTRL.instance.washKit_num--;
                    PlayerCTRL.instance.cur_tool = ToolType.empty;
                    return true;
                }
                break;
            default:
                return false;
                break;
        }
        return false;
    }

    public void RandomChoiceMechTools(){
      // 机械的损伤，从Hammer,spanner, screwer中挑选
      // 会从 Hammer, spanner, screwer中挑选
        int rand_choice = Random.Range(0,3);
        switch(rand_choice){
          case 0:
            hammer_required++;
            break;
          case 1:
            spanner_required++;
            break;
          case 2:
            screwer_required++;
            break;
          default:
            break;
        }
        return;
    }

    public void RandomChoiceDecorateTools(){
      // 机械的损伤，从Hammer,spanner, screwer中挑选
      // 会从 Hammer, spanner, screwer中挑选
        int rand_choice = Random.Range(0,2);
        switch(rand_choice){
          case 0:
            sewingKit_required++;
            break;
          case 1:
            washKit_required++;
            break;
          default:
            break;
        }
        return;
    }

    public void RandomChoiceLifeTools(){

    }

    public virtual void DrawUI()
    {
        int index = 0;
        foreach (var b in listBubbles)
        {
            b.GetComponent<bubble>().SetInvisible();
        }
        if (sewingKit_required > 0)
        {
            if (index == listBubbles.Count) { listBubbles[index].GetComponent<bubble>().SetToolsMore();}
            else { listBubbles[index++].GetComponent<bubble>().SetTool(ToolType.sewingKit); }
        }
        if (hammer_required > 0)
        {
            Debug.Log("hammer require");
            if (index == listBubbles.Count) { listBubbles[index].GetComponent<bubble>().SetToolsMore();}
            else { listBubbles[index++].GetComponent<bubble>().SetTool(ToolType.hammer); }
        }
        if (screwer_required > 0)
        {
            if (index == listBubbles.Count) { listBubbles[index].GetComponent<bubble>().SetToolsMore();}
            else { listBubbles[index++].GetComponent<bubble>().SetTool(ToolType.screwer); }
        }
        if (spanner_required > 0)
        {
            if (index == listBubbles.Count) { listBubbles[index].GetComponent<bubble>().SetToolsMore();}
            else { listBubbles[index++].GetComponent<bubble>().SetTool(ToolType.spanner); }
        }
        if (washKit_required > 0)
        {
            if (index == listBubbles.Count) { listBubbles[index].GetComponent<bubble>().SetToolsMore();}
            else { listBubbles[index++].GetComponent<bubble>().SetTool(ToolType.washKit); }
        }
        Debug.Log(index);
        for(int i = 0; i < index; i++)
        {
            listBubbles[i].GetComponent<bubble>().SetVisible();
        }
    }

    public virtual void OnPlayerExit() { }

}
