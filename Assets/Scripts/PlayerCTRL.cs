using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum playerState
{
    idle,
    work,
    repair,
    summonCat,
    servingCat, // 和猫玩耍时的状态，在猫玩耍玩之前，无法解除
}

public enum ToolType
{
    empty,
    sewingKit, //1
    hammer, //2
    screwer, //3
    spanner, //4
    washKit, //5
    band, //6
    catToy
}

public class PlayerCTRL : MonoBehaviour
{
    public static PlayerCTRL instance;
    [SerializeField] int money;
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float interactRange = 1.0f;

    // 拥有的工具的数量
    [SerializeField] public int sewingKit_num = 5;
    [SerializeField] public int hammer_num = 5;
    [SerializeField] public int screwer_num = 5;
    [SerializeField] public int spanner_num = 5;
    [SerializeField] public int washKit_num = 5;
    [SerializeField] public int band_num = 5;
    [SerializeField] public int club_num = 5;
    [SerializeField] public ToolType cur_tool = ToolType.empty;

    SpriteRenderer sr;
    Animator animator;
    playerState state = playerState.idle;
    List<InteractiveEntity> attachedEntities = new List<InteractiveEntity>();
    bool inComputer = false;

    [SerializeField] GameObject cattoy;

    public void AddMoney(int value)
    {
        this.money += value;
    }

    public int GetMoney() { return this.money; }
    public playerState GetState() {return this.state; }
    public void SetState(playerState wished_state) {this.state=wished_state;}
    public int GetToolNum(int index)
    {
        switch (index)
        {
            case 1:
                return sewingKit_num;
            case 2:
                return hammer_num;
            case 3:
                return screwer_num;
            case 4:
                return spanner_num;
            case 5:
                return washKit_num;
            case 6:
                return club_num;
            default:
                break;
        }
        return -1;
    }

    public int AddToolNum(int index, int value)
    {
        switch (index)
        {
            case 1:
                sewingKit_num += value;
                break;
            case 2:
                hammer_num += value;
                break;
            case 3:
                screwer_num += value;
                break;
            case 4:
                spanner_num += value;
                break;
            case 5:
                washKit_num += value;
                break;
            case 6:
                club_num += value;
                break;
            default:
                break;
        }
        return -1;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        this.cur_tool = ToolType.empty;
        sr = this.GetComponentInChildren<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if( cur_tool == ToolType.empty){
            state = playerState.idle;
        }
        if( state == playerState.servingCat){
            UpdateAnim(0, 0);
            return;
        }
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(cur_tool == ToolType.catToy){
                state = playerState.summonCat;
            }
            else
            {
                state = playerState.idle;

                Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, interactRange);
                foreach (var item in colliders)
                {
                    if (item.tag != "interact") continue;
                    item.GetComponent<InteractiveEntity>().OnInteract(KeyCode.E);
                    break;
                }
            }
        }else if (Input.GetKeyDown(KeyCode.F))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, interactRange);
            foreach (var item in colliders)
            {
                if (item.tag != "interact") continue;
                item.GetComponent<InteractiveEntity>().OnInteract(KeyCode.F);
                break;
            }
        }else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(cur_tool == ToolType.sewingKit){
                cur_tool = ToolType.empty;
            }
            
            else if(sewingKit_num > 0){
              cur_tool = ToolType.sewingKit;
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            
            if(cur_tool == ToolType.hammer){
                cur_tool = ToolType.empty;
            }
            
            else if(hammer_num > 0){
                cur_tool = ToolType.hammer;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(cur_tool == ToolType.screwer){
                cur_tool = ToolType.empty;
            }
            
            else if(screwer_num > 0){
                cur_tool = ToolType.screwer;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if(cur_tool == ToolType.spanner){
                cur_tool = ToolType.empty;
            }
            
            else if(spanner_num > 0){
                cur_tool = ToolType.spanner;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if(cur_tool == ToolType.washKit){
                cur_tool = ToolType.empty;
            }
            
            else if(washKit_num > 0)
            {
                cur_tool = ToolType.washKit;
            }
        }
        //else if (Input.GetKeyDown(KeyCode.Alpha6))
        //{
        //    if(cur_tool == ToolType.band){
        //        cur_tool = ToolType.empty;
        //    }
            
        //    else if(band_num > 0)
        //    {
        //        cur_tool = ToolType.band;
        //    }
        //}
        else if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            if(cur_tool == ToolType.catToy){
                cur_tool = ToolType.empty;
            }
            else if(band_num > 0)
            {
                cur_tool = ToolType.catToy;
            }
        }
        

        
        Vector2 dV = new Vector2(dx, dy);
        
        this.transform.Translate(dV * Time.deltaTime * moveSpeed);
        UpdateAnim(dx, dy);
    }

    void UpdateAnim(float dx, float dy)
    {
        sr.flipX = dx > 0;
        if (Mathf.Abs(dx) < 0.1f && Mathf.Abs(dy) < 0.1f)
        {
            animator.SetInteger("playerState", 0);
        }
        else
        {
            if (Mathf.Abs(dx) > Mathf.Abs(dy)) animator.SetInteger("playerState", 1);
            else if (dy > 0) animator.SetInteger("playerState", 2);
            else animator.SetInteger("playerState", 3);
        }
        if (state == playerState.summonCat || state == playerState.servingCat)
            cattoy.SetActive(true);
        else
            cattoy.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "interact")
        {
            if (collision.name == "computer")
                inComputer = true;
            animator.SetBool("inComputer", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "interact")
        {
            if (collision.name == "computer")
                inComputer = false;
            animator.SetBool("inComputer", false);
        }
    }
}
