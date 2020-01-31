using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class computer : InteractiveEntity
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

    public override void OnInteract(KeyCode keyCode)
    {
        if(keyCode == KeyCode.E)
        {
            PlayerCTRL.instance.AddMoney(perGenMoney);
        }
    }
}
