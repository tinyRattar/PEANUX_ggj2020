﻿using System.Collections;
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
        SetDurabilityMax();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenShop()
    {
        ui_shop.SetActive(true);
        shopOpen = true;
    }

    void CloseShop()
    {
        ui_shop.SetActive(false);
        shopOpen = false;
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
            BGMManager.Instance.setBGM(0);
            //SEManager.Instance.PlaySE(0);
            return false;
        }else if(keyCode == KeyCode.F)
        {
            if(shopOpen)
                CloseShop();
            else
                OpenShop();
            return false;
        }
        return false;
    }

    public override void OnPlayerExit()
    {
        base.OnPlayerExit();
        CloseShop();
    }
}
