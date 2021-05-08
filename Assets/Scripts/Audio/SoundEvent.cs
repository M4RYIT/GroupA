using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundData
{
    public string Name;
    public AudioClip Clip;
    public float Volume;
}

public class SoundEvent : MonoBehaviour
{
    public AudioSource Source;

    [SerializeField]
    List<SoundData> Sounds;
    SoundData sd;

    public void PlayOneShot(string name)
    {              
        sd = Sounds.Find(s => s.Name == name);
        Source.PlayOneShot(sd.Clip, sd.Volume);
    }

    public void Play(string name)
    {
        sd = Sounds.Find(s => s.Name == name);
        if (sd.Clip != Source.clip) Source.clip = sd.Clip;
        Source.volume = sd.Volume;
        Source.Play();        
    }

    public void Play()
    {
        foreach (var source in GetComponents<AudioSource>())
        {
            source.Play();
        }
    }

    public void Pause()
    {
        foreach (var source in GetComponents<AudioSource>())
        {
            source.Play();
        }
    }

    public void Stop()
    {
        foreach (var source in GetComponents<AudioSource>())
        {
            source.Stop();
        }        
    }
}
