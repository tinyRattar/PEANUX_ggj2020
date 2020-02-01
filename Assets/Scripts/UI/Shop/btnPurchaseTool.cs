using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnPurchaseTool : btnPurchase
{
    [SerializeField] ToolType targetTool;
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
        Debug.LogError("todo: no implement");
    }
}
