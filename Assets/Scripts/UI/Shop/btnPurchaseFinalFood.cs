using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("You Win!!!!!!!!");
    }
}
