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
    CatState state = CatState.idle;
    float timer = 0f;
    Vector2 direction = Vector2.zero;
    float moveTime = 1.0f;
    int walkedCount = 0;
    GameObject targetAttack;
    [SerializeField] int maxRandomWalk = 4;
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float attackRange = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        NextActionDecide();
    }

    // cat behave
    void CatWalk()
    {

        // check attack
        // this.transform.Translate(v)
        // if moveover then NextActionDecide()
    }

    void NextActionDecide()
    {
        if(walkedCount > maxRandomWalk)
        {
            // decide to attack
            Collider2D[] colliderCollection = Physics2D.OverlapCircleAll(this.transform.position, attackRange);
            foreach (var collider in colliderCollection)
            {
                if(collider.tag == "interact")
                {
                    targetAttack = collider.gameObject;
                }
            }
        }
        float r = Random.Range(0, 1.0f);
        if (r < 0.5f)
        {

        }

        float angle = Random.Range(0, 360);
        direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        moveTime = Random.Range(1f, 2f);
        timer = moveTime;
        // get random direction
        // start attack if available  play attack anim. CatAttack()
        // set timer
    }

    void CatAttack(InteractiveEntity target, CatAttackType attackType)
    {
        target.OnCatInteract(attackType);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case CatState.idle:
                // idle timer
                // if idleover then NextActionDecide()
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
        CatWalk();
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
