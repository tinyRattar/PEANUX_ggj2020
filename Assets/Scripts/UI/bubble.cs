using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubble : MonoBehaviour
{
    [SerializeField] SpriteRenderer srTool;
    [SerializeField] SpriteRenderer srBubble;

    public void SetTool(ToolType tool)
    {
        switch (tool)
        {
            case ToolType.empty:
                break;
            case ToolType.sewingKit:
                //srTool.sprite = Resources.Load<Sprite>("ui/sewingkit_01");
                srTool.sprite = ResourceManager.instance.sprites[0];
                break;
            case ToolType.hammer:
                Debug.Log("hammer icon");
                //srTool.sprite = Resources.Load<Sprite>("ui/harmmer_01");
                srTool.sprite = ResourceManager.instance.sprites[1];
                break;
            case ToolType.screwer:
                //srTool.sprite = Resources.Load<Sprite>("ui/screwer_01");
                srTool.sprite = ResourceManager.instance.sprites[2];
                break;
            case ToolType.spanner:
                srTool.sprite = ResourceManager.instance.sprites[3];
                break;
            case ToolType.washKit:
                srTool.sprite = ResourceManager.instance.sprites[4];
                break;
            case ToolType.band:
                break;
            default:
                break;
        }
    }

    public void SetToolsMore()
    {
        srTool.sprite = Resources.Load<Sprite>("ui/screwer_01.png"); //todo: icon for more
    }

    public void SetInvisible()
    {
        srBubble.color = new Color(1f, 1f, 1f, 0f);
        srTool.color = new Color(1f, 1f, 1f, 0f);
    }

    public void SetVisible()
    {
        srBubble.color = new Color(1f, 1f, 1f, 1f);
        srTool.color = new Color(1f, 1f, 1f, 1f);
    }

    public void OnRepair()
    {
        Debug.Log("todo: repair anim");
        Destroy(this.gameObject, 0.5f);
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
