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
    // Start is called before the first frame update
    void Start()
    {
        
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
