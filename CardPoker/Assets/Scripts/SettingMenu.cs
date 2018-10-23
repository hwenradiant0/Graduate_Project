using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;


    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void Mute()
    {
        slider.value = -80.0f;
        audioMixer.SetFloat("volume", -80.0f);
    }

    public void Onsound()
    {
        slider.value = 10.0f;
        audioMixer.SetFloat("volume", 0.0f);
    }
}
