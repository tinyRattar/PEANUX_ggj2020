using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEPlayer : MonoBehaviour
{
    AudioSource audioSource;
    bool startPlay = false;

    public void init(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        startPlay = true;
    }

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying && startPlay)
        {
            Destroy(this.gameObject, 0.5f);
        }
    }
}
