using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioData
{
    public string ParamName;
    public float ParamValue;

    public AudioData(string nm)
    {
        ParamName = nm;
    }
}

public class AudioSetter : MonoBehaviour
{
    public AudioMixer AudioMixer;
    public string ParamName;
    public UIColorSwitch UIColorSwitch;

    AudioData AudioData;

    // Start is called before the first frame update
    void Start()
    {
        AudioData = new AudioData(ParamName);
        AudioMixer.GetFloat(ParamName, out AudioData.ParamValue);
        UIColorSwitch.SwitchColor((int)(AudioData.ParamValue / -80));
    }

    public void SetAudio()
    {
        AudioData.ParamValue = -80 - (AudioData.ParamValue);
        AudioMixer.SetFloat(AudioData.ParamName, AudioData.ParamValue);
        UIColorSwitch.SwitchColor((int)(AudioData.ParamValue / -80));
    }
}
