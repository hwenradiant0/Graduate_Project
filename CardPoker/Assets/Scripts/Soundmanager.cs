using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

    public AudioMixerGroup audioMixer;
    public Sound[] sounds;

	// Use this for initialization
	void Awake ()
    {
		foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.outputAudioMixerGroup = audioMixer;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
	}


    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            Play("MenuTheme");
        }
        else
            Play("MainTheme");
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}