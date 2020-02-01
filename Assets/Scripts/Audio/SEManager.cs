using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    public static SEManager Instance;
    [SerializeField] GameObject sePlayer;
    [SerializeField] List<AudioClip> clips;

    public void PlaySE(int index)
    {
        GameObject go = GameObject.Instantiate(sePlayer, this.transform);
        go.GetComponent<SEPlayer>().init(clips[index]);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More Than two instance");
        }
    }
}
