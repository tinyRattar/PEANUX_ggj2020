using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class InteractiveEntity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public void OnInteract(KeyCode keyCode)
    {

    }

    public virtual void OnCatInteract(CatAttackType type)
    {

    }
}
