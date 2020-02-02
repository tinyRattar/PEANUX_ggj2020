using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    [SerializeField] Text uiMoney;
    [SerializeField] List<uiTool> uiTools;
    [SerializeField] Image processBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        uiMoney.text = PlayerCTRL.instance.GetMoney().ToString();
        for(int i = 0; i < 6; i++)
        {
            uiTools[i].SetText(PlayerCTRL.instance.GetToolNum(i+1).ToString());
            if ((int)PlayerCTRL.instance.cur_tool - 1 == i)
                uiTools[i].EnSelect();
            else
                uiTools[i].DisSelect();
        }
        if (PlayerCTRL.instance.cur_tool == ToolType.catToy)
            uiTools[5].EnSelect();
        processBar.fillAmount = InteractiveEntity.total_durability / InteractiveEntity.total_durability_max;
    }
}
