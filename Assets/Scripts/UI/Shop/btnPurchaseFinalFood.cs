﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btnPurchaseFinalFood : btnPurchase
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void DealPurchaseSucess()
    {
        Debug.Log("bought");
        base.DealPurchaseSucess();
        SceneManager.LoadScene(3);
    }
}
