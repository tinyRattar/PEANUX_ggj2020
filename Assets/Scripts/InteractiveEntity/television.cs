using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Television : InteractiveEntity
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool OnInteract(KeyCode keyCode)
    {
        return false;
    }

    public override void AddRequiredTool(){
        hammer_required++;
        screwer_required++;
    }
}
