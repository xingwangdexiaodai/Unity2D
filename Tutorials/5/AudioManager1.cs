using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0f, 1f)]
    public float pitch = 0.7f;

    private AudioSource theAS;

    public void SetUpSound(AudioSource _theAS) {
        theAS = _theAS;
        theAS.clip = clip;
    }

    public void Play() {
        theAS.pitch = pitch;
        theAS.volume = volume;
        theAS.Play();
    }
}

public class AudioManager1 : MonoBehaviour
{
    public static AudioManager1 instance;

    [SerializeField]
    private Sound[] sounds;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        foreach (var sound in sounds) {
            sound.SetUpSound(new GameObject().AddComponent<AudioSource>());
        }
    }

    public void Play(string _name) {
        foreach(var sound in sounds){ 
            if(sound.name == _name) {
                sound.Play();
            }
        }
    }
}
