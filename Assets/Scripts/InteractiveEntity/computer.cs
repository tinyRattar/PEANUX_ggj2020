using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : InteractiveEntity
{
    [SerializeField] int perGenMoney = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 纯虚函数，家具的最大耐久度，每个家具不一样
    public virtual void SetDurabilityMax(){
        durability_max = 99999.0f;
    }

    public override bool OnInteract(KeyCode keyCode)
    {
        if(keyCode == KeyCode.E)
        {
            PlayerCTRL.instance.AddMoney(perGenMoney);
        }

        return true;
    }
}
