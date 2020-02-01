using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum playerState
{
    idle,
    work,
    repair
}

public enum ToolType
{
    empty,
    sewingKit, //1
    hammer, //2
    screwer, //3
    spanner, //4
    washKit, //5
    band //6
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

    [SerializeField] public ToolType cur_tool = ToolType.empty;

    playerState state = playerState.idle;
    List<InteractiveEntity> attachedEntities = new List<InteractiveEntity>();

    public void AddMoney(int value)
    {
        this.money += value;
    }

    public int GetMoney() { return this.money; }

    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        this.cur_tool = ToolType.empty; 
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (var item in attachedEntities)
            {
                item.OnInteract(KeyCode.E);
                break;
            }
        }else if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (var item in attachedEntities)
            {
                item.OnInteract(KeyCode.F);
                break;
            }
        }else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(sewingKit_num > 0){
              cur_tool = ToolType.sewingKit;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            
            if(hammer_num > 0){
                cur_tool = ToolType.hammer;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(screwer_num > 0){
                cur_tool = ToolType.screwer;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if(spanner_num > 0){
                cur_tool = ToolType.spanner;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if(washKit_num > 0)
            {
                cur_tool = ToolType.washKit;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if(band_num > 0)
            {
                cur_tool = ToolType.band;
            }
        }
        

        
        Vector2 dV = new Vector2(dx, dy);
        this.transform.Translate(dV * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "interact")
        {
            attachedEntities.Add(collision.GetComponent<InteractiveEntity>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "interact")
        {
            InteractiveEntity cie = collision.GetComponent<InteractiveEntity>();
            attachedEntities.Remove(cie);
            cie.OnPlayerExit();
        }
    }
}
