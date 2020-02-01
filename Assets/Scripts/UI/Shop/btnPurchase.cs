using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnPurchase : MonoBehaviour
{
    [SerializeField] int price;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void Init()
    {
        this.GetComponentInChildren<Text>().text = "$ " + price.ToString();
    }

    public virtual void DealPurchase() { }

    public void OnClick()
    {
        if(PlayerCTRL.instance.GetMoney() >= price)
        {
            PlayerCTRL.instance.AddMoney(-price);
            DealPurchase();
            Debug.Log("purchase success");
        }
        else
        {
            Debug.Log("we need more gold");
        }
    }
}
