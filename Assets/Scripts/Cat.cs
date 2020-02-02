using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CatAttackType
{
    scratch,
    bite,
    pee
}

public enum CatState
{
    idle,
    walk,
    attack,
    directlywalk,
    beLoved,
    fooled
}

public class Cat : MonoBehaviour
{
    [SerializeField] CatState state = CatState.idle;
    static float catPlayTimerValue = 5.0f;

    [SerializeField] float timer = 0f;
    [SerializeField] Vector2 direction = Vector2.zero;
    [SerializeField] float moveTime = 1.0f;
    [SerializeField] float attackTime = 1.0f;
    [SerializeField] int walkedCount = 0;
    [SerializeField] int fooled_nums = 0;
    bool started_run_to_player_flag = false;
    bool neared_player_flag = false;
    bool is_dice_rolled = false;
    GameObject targetAttack;
    [SerializeField] int leastRandomWalk = 4;
    [SerializeField] float attackProb = 0.8f;

    //[SerializeField] float baseProb = 0.4f;
    [SerializeField] float perWalkProb = 0.2f;
    [SerializeField] float restProb = 0.2f;
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float attackRange = 1.0f;
    [SerializeField] float catPlayTimer = catPlayTimerValue;
    [SerializeField] float directlyWalkProb = 0.5f;

    bool needCatMeow = true;

    [SerializeField] Transform furniture;
    int furnitureCount;
    // Start is called before the first frame update
    void Start()
    {
        NextActionDecide();
        furniture = GameObject.Find("Furniture").transform;
        furnitureCount = furniture.childCount;
    }

    // cat behave
    void CatWalk()
    {
        Vector2 nextPos = this.transform.position;
        nextPos += direction * moveSpeed * Time.deltaTime;
        //if (TryAttack(nextPos))
        //{
        //    return;
        //}
        this.transform.position = nextPos;
    }

    void NextActionDecide()
    {
        float r1 = 0f; // probability of attack
        float r2 = Random.Range(0, 1.0f); // probability of rest

        if (walkedCount >= leastRandomWalk)
        {
            float p = Random.Range(0.5f, 1f);
            p = 1f;
            r1 = (1 + walkedCount) * perWalkProb * p;
        }
        if (r1 > attackProb)
        {
            if (TryAttack(this.transform.position))
            {
                walkedCount = 0;
                return;
            }
        }
        // if didn't attack
        if (r2 > restProb)
        {
            float r3 = Random.Range(0f, 1f);
            if (r3 >= directlyWalkProb)
            {
                float angle = Random.Range(0, 360);
                direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                moveTime = Random.Range(1f, 2f);
                state = CatState.walk;
            }
            else
            {
                Vector3 nextFurniturePosition = furniture.GetChild(Random.Range(0, furnitureCount - 1)).position;
                moveTime = (nextFurniturePosition - transform.position).magnitude / moveSpeed;
                direction = (nextFurniturePosition - transform.position) / (moveSpeed * moveTime);
                state = CatState.directlywalk;
            }
            timer = moveTime;
            walkedCount++;
        }
        else
        {
            float idleTime = Random.Range(0.5f, 1.5f);
            timer = idleTime;
            state = CatState.idle;
        }
    }

    bool TryAttack(Vector2 pos, CatAttackType attackType = CatAttackType.bite)
    {
        Collider2D[] colliderCollection = Physics2D.OverlapCircleAll(pos, attackRange);
        foreach (var collider in colliderCollection)
        {
            if (collider.tag == "interact")
            {
                targetAttack = collider.gameObject;
                InteractiveEntity targetAttack_ie = targetAttack.GetComponent<InteractiveEntity>();
                if (targetAttack_ie.AttackCheck())
                {
                    CatAttack(targetAttack_ie, CatAttackType.bite);
                    return true;
                }
            }
        }
        return false;
    }

    void CatAttack(InteractiveEntity target, CatAttackType attackType)
    {
        target.OnCatInteract(attackType);
        state = CatState.attack;
        timer = attackTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckFooled();
        switch (state)
        {
            case CatState.idle:
                needCatMeow=true;
                // pass
                break;
            case CatState.walk:
                needCatMeow=true;
                CatWalk();
                break;
            case CatState.directlywalk:
                needCatMeow=true;
                CatWalk();
                break;
            case CatState.attack:
                needCatMeow=true;
                // attack timer
                // if attackover then NextActionDecide()
                break;
            case CatState.beLoved:
                OnBeLoved();
                // Debug.Log("In Love");
                if(needCatMeow){
                  Debug.Log("meow");
                  SEManager.Instance.PlaySE(22);
                  needCatMeow=false;
                }

                return;
                break;
            case CatState.fooled:
                UpdateFoolNums();
                needCatMeow=true;
                break;
            default:
                break;
        }
        timer -= Time.deltaTime;
        if (timer < 0)
            NextActionDecide();

        // ������ڶ�è
        if (PlayerCTRL.instance.GetState() == playerState.summonCat)
        {
            // var ignore_probability = 0.0f + fooled_nums*0.2;
            // if(ignore_probability >=0.8){
            //     ignore_probability=0.8;
            // }
            // var roll_dice = Random.Range(0.0f,1.0f) + 0.001f;
            // if(roll_dice<ignore_probability){
            //     is_dice_rolled = true;
            //     PlayerCTRL.instance.SetState(playerState.idle);
            //     // ��������
            //     return;
            // }


            if (!NearPlayer(PlayerCTRL.instance.gameObject.transform.position))
            {
                RunToPlayer(PlayerCTRL.instance.gameObject.transform.position);

            }
            else
            {
                catPlayTimer = catPlayTimerValue;
                PlayerCTRL.instance.club_num--;
                PlayingWithPlayer(PlayerCTRL.instance.cur_tool);
                fooled_nums = 0;
            }
        }
    }

    void CheckFooled()
    {
        // if(started_run_to_player_flag==true && neared_player_flag==false){
        //     state = CatState.fooled;
        // }
    }

    void UpdateFoolNums()
    {
        // fooled_nums++;
        // started_run_to_player_flag = false;
        // neared_player_flag = false;
        // state = CatState.idle;
    }
    bool NearPlayer(Vector3 playerPos)
    {
        Vector2 desPos = playerPos;
        Vector2 curPos = this.transform.position;
        var distance = (desPos - curPos).magnitude;
        if (distance < 0.6)
        {
            return true;
        }
        return false;
    }

    void RunToPlayer(Vector3 playerPos)
    {
        started_run_to_player_flag = true;
        Vector2 desPos = playerPos;
        Vector2 curPos = this.transform.position;
        Vector2 direction = (desPos - curPos).normalized;
        Vector2 nextPos = curPos + direction * 4 * moveSpeed * Time.deltaTime;

        this.transform.position = nextPos;
    }

    void PlayingWithPlayer(ToolType tool)
    {
        neared_player_flag = true;

        if (tool == ToolType.catToy)
        {
            // meow
            // playing animation
            state = CatState.beLoved;
        }
        else
        {
            return;
        }

        // start meowing 


    }



    public void OnBeLoved()
    {
        PlayerCTRL.instance.SetState(playerState.servingCat);
        // change timer
        catPlayTimer = catPlayTimer - Time.deltaTime;
        if (catPlayTimer < 0)
        {
            state = CatState.walk;
            PlayerCTRL.instance.SetState(playerState.idle);
            PlayerCTRL.instance.cur_tool = ToolType.empty;
            Debug.Log("idle player");
        }
    }

    public void ExitBeLoved()
    {
        state = CatState.idle;
        NextActionDecide();
    }
}
