using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    [SerializeField] Text uiMoney;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        uiMoney.text = PlayerCTRL.instance.GetMoney().ToString();
    }
}
