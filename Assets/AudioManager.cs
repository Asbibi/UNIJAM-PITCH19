
using UnityEngine;
using UnityEngine.Audio;
using System;


public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;
    void Awake()
    {
        foreach (Sounds s in sounds)
        {
         s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

        }
    }

    // Update is called once per frame
   public void Play (string name)
    {
       Sounds s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
