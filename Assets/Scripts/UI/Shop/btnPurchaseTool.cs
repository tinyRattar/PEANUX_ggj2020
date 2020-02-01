using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnPurchaseTool : btnPurchase
{
    [SerializeField] ToolType targetTool;
    [SerializeField] int addNum = 3;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void DealPurchase()
    {
        base.DealPurchase();
        PlayerCTRL.instance.AddToolNum((int)targetTool, addNum);
        // Debug.LogError("todo: no implement");
    }
}
