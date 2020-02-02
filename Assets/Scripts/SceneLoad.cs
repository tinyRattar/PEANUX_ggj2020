using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    [SerializeField] bool clickContinue;
    [SerializeField] int indexScene;
    [SerializeField] bool keyContinue;
    [SerializeField] KeyCode waitKey;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (clickContinue)
        {
            if (Input.GetMouseButtonDown(0))
                SceneManager.LoadScene(indexScene);
        }
        if (keyContinue)
        {
            if (Input.GetKeyDown(waitKey))
                SceneManager.LoadScene(indexScene);
        }
    }
}
