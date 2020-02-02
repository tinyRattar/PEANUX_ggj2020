using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

abstract public class InteractiveEntity : MonoBehaviour
{
    [SerializeField] static public float total_durability = 0.0f;
    [SerializeField] static public float total_durability_max = 0.0f;
    [SerializeField] public float durability = 30;
    [SerializeField] public float durability_max = 20;
    float brokenThresh = 0.9f;
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
    [SerializeField] Sprite spGood;
    [SerializeField] Sprite spBroken;
    [SerializeField] SpriteRenderer sr; 
    public void Init()
    {
        SetDurabilityMax();
        durability = durability_max;
        InteractiveEntity.total_durability += durability_max * 0.2f;
        total_durability_max += durability_max * 0.2f;
        DrawUI();
        bubble[] bubbles = this.GetComponentsInChildren<bubble>();
        listBubbles = new List<GameObject>();
        foreach (var item in bubbles)
        {
            listBubbles.Add(item.gameObject);
        }
        //sr = this.GetComponentInChildren<SpriteRenderer>();
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
        total_durability -= uniform_damage;
        switch (type){
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
            SEManager.Instance.PlaySE(20);

            durability = 0;
            OnTotalDamage();
        }
        else{
            SEManager.Instance.PlaySE(19);

            if(type == CatAttackType.pee){
                AddCleaningTool();
            }
            else{
                AddRequiredTool();
            }
        }
        DrawUI();
        if (total_durability < 0)
        {
            SceneManager.LoadScene(2);
        }
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
        if(isRequiredTool(tool)){
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
                    total_durability += uniform_damage * 0.9f;
                    PlayerCTRL.instance.sewingKit_num--;
                    PlayerCTRL.instance.cur_tool = ToolType.empty;
                    SEManager.Instance.PlaySE(25);
                    return true;
                }
                break;
            case ToolType.hammer:
                if (hammer_required>0){
                    hammer_required--;
                    durability += uniform_damage*0.9f;
                    total_durability += uniform_damage * 0.9f;
                    PlayerCTRL.instance.hammer_num--;
                    PlayerCTRL.instance.cur_tool = ToolType.empty;
                    SEManager.Instance.PlaySE(23);
                    return true;
                }
                break;
            case ToolType.screwer:
                if (screwer_required>0){
                    screwer_required--;
                    durability += uniform_damage*0.9f;
                    total_durability += uniform_damage * 0.9f;
                    PlayerCTRL.instance.screwer_num--;
                    PlayerCTRL.instance.cur_tool = ToolType.empty;
                    SEManager.Instance.PlaySE(23);
                    return true;
                }
                break;
            case ToolType.spanner:
                if (spanner_required>0){
                    spanner_required--;
                    durability += uniform_damage*0.9f;
                    total_durability += uniform_damage * 0.9f;
                    PlayerCTRL.instance.spanner_num--;
                    PlayerCTRL.instance.cur_tool = ToolType.empty;
                    SEManager.Instance.PlaySE(24);
                    return true;
                }
                break;
            case ToolType.washKit:
                if (washKit_required > 0)
                {
                    washKit_required--;
                    durability += uniform_damage * 0.9f;
                    total_durability += uniform_damage * 0.9f;
                    PlayerCTRL.instance.washKit_num--;
                    PlayerCTRL.instance.cur_tool = ToolType.empty;
                    SEManager.Instance.PlaySE(21);
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
            if (index == listBubbles.Count) { listBubbles[index].GetComponent<bubble>().SetToolsMore(); }
            else { listBubbles[index++].GetComponent<bubble>().SetTool(ToolType.sewingKit); }
        }
        if (hammer_required > 0)
        {
            if (index == listBubbles.Count) { listBubbles[index].GetComponent<bubble>().SetToolsMore(); }
            else { listBubbles[index++].GetComponent<bubble>().SetTool(ToolType.hammer); }
        }
        if (screwer_required > 0)
        {
            if (index == listBubbles.Count) { listBubbles[index].GetComponent<bubble>().SetToolsMore(); }
            else { listBubbles[index++].GetComponent<bubble>().SetTool(ToolType.screwer); }
        }
        if (spanner_required > 0)
        {
            if (index == listBubbles.Count) { listBubbles[index].GetComponent<bubble>().SetToolsMore(); }
            else { listBubbles[index++].GetComponent<bubble>().SetTool(ToolType.spanner); }
        }
        if (washKit_required > 0)
        {
            sr.color = new Color(1f, 1f, 1 - washKit_required / 3f);
            if (index == listBubbles.Count) { listBubbles[index].GetComponent<bubble>().SetToolsMore(); }
            else { listBubbles[index++].GetComponent<bubble>().SetTool(ToolType.washKit); }
        }
        else
        {
            sr.color = new Color(1f, 1f, 1f);
        }
        for (int i = 0; i < index; i++)
        {
            listBubbles[i].GetComponent<bubble>().SetVisible();
        }

        if (durability / durability_max < brokenThresh)
        {
            sr.sprite = spBroken;
        }
        else
        {
            sr.sprite = spGood;
        }
    }

    public virtual void OnPlayerExit() { }

}
