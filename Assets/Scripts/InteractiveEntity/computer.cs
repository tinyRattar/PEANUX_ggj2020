using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : InteractiveEntity
{
    [SerializeField] int perGenMoney = 1;
    [SerializeField] GameObject ui_shop;
    bool shopOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenShop()
    {
        ui_shop.SetActive(true);
    }

    void CloseShop()
    {
        ui_shop.SetActive(false);
    }

    public override bool OnInteract(KeyCode keyCode)
    {
        if(keyCode == KeyCode.E)
        {
            PlayerCTRL.instance.AddMoney(perGenMoney);
            return false;
        }else if(keyCode == KeyCode.F)
        {
            if(shopOpen)
                OpenShop();
            else
                CloseShop();
            return false;
        }
        return false;
    }
}
