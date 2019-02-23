using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource theAS;

    private void Awake()
    {
        if(instance == null) {
            instance = this;
        }
        else if(instance != this){
            Destroy(gameObject);
        }

        theAS = GetComponent<AudioSource>();
    }

    public void Play(string _audio) {
        var audioClip = Resources.Load<AudioClip>(_audio);
        // PlayOneShot **NOT** Play
        theAS.PlayOneShot(audioClip);
    }
}
