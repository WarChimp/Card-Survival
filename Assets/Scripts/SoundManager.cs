using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;
    public AudioSource[] audioSounds;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound(int soundToPlay)
    {
        audioSounds[soundToPlay].Play();
    }
}
