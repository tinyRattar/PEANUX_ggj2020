using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fake3DbyAxisZ : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LastUpdate()
    {
        Vector3 oldPos = this.transform.position;
        oldPos.z = oldPos.y;
        this.transform.position = oldPos;
    }
}
