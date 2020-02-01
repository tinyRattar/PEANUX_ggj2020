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
    beLoved
}

public class Cat : MonoBehaviour
{
    [SerializeField]CatState state = CatState.idle;
    float timer = 0f;
    Vector2 direction = Vector2.zero;
    float moveTime = 1.0f;
    float attackTime = 1.0f;
    int walkedCount = 0;
    GameObject targetAttack;
    [SerializeField] int leastRandomWalk = 4;
    [SerializeField] float attackProb = 0.8f;
    
    //[SerializeField] float baseProb = 0.4f;
    [SerializeField] float perWalkProb = 0.2f;
    [SerializeField] float restProb = 0.2f;
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float attackRange = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        NextActionDecide();
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
            r1 = walkedCount * perWalkProb * p;
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
        if (r2 >= restProb)
        {
            float angle = Random.Range(0, 360);
            direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            moveTime = Random.Range(1f, 2f);
            timer = moveTime;
            state = CatState.walk;
            walkedCount++;
        }
        else
        {
            float idleTime = Random.Range(1f, 2f);
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
    void Update()
    {
        switch (state)
        {
            case CatState.idle:
                // pass
                break;
            case CatState.walk:
                CatWalk();
                break;
            case CatState.attack:
                // attack timer
                // if attackover then NextActionDecide()
                break;
            case CatState.beLoved:
                break;
            default:
                break;
        }
        timer -= Time.deltaTime;
        if (timer < 0)
            NextActionDecide();
    }

    public void OnBeLoved()
    {
        if (state != CatState.attack)
        {
            state = CatState.beLoved;
        }
    }

    public void ExitBeLoved()
    {
        state = CatState.idle;
        NextActionDecide();
    }
}
