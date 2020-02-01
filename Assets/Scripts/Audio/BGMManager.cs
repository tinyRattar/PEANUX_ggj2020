using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour {
    public static BGMManager Instance;
	[SerializeField]List<AudioClip> clips;
	AudioSource audio;

	public void setBGM(int i){
		audio.clip = clips [i];
		audio.Play ();

	}
	// Use this for initialization
	void Start () {
		audio = this.GetComponent<AudioSource> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
