using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiTool : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Image selector;

    public void EnSelect()
    {
        selector.color = new Color(1f, 1f, 1f, 1f);
    }

    public void DisSelect()
    {
        selector.color = new Color(1f, 1f, 1f, 0f);
    }

    public void SetText(string value)
    {
        text.text = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
