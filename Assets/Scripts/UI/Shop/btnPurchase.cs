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

    public virtual void DealPurchaseSucess()
    {
        SEManager.Instance.PlaySE(1);
    }
    public virtual void DealPurchaseFail()
    {
        SEManager.Instance.PlaySE(14);
    }

    public void OnClick()
    {
        if(PlayerCTRL.instance.GetMoney() >= price)
        {
            PlayerCTRL.instance.AddMoney(-price);
            DealPurchaseSucess();
            Debug.Log("purchase success");
        }
        else
        {
            DealPurchaseFail();
            Debug.Log("we need more gold");
        }
    }
}
