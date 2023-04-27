 using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;

using System;


public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sounds;
    private static AudioManager instance;
    public float sceneTransitionTime = 0.4f;

    /* Mixer was controlled to edit its settings via options menu and PlayerPrefs */
    //public AudioMixerGroup audioMixerGroup;
    //public AudioMixer audioMixer;
    //public OptionsLoader optionsLoader;

    private void Start()
    {
        //audioMixer.SetFloat("volume", 100f);
        foreach (Sound sound in sounds)
        {
            //Debug.Log("initializing sounds");
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            //sound.source.outputAudioMixerGroup = audioMixerGroup;
        }
        StartCoroutine(playMainTheme());
    }

    IEnumerator playMainTheme()
    {
        yield return new WaitForSeconds(sceneTransitionTime);
        Play("Main Theme");
    }

    public void Update()
    {
        if(SceneManager.GetActiveScene().name == "GameOver")
        {
            Pause("Main Theme");
        }else //if(SceneManager.GetActiveScene().name != "GameOver")
        {
            UnPause("Main Theme");
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(sounds == null){
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (sounds == null)
        {
            return;
        }
        s.source.Stop();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (sounds == null)
        {
            return;
        }
        s.source.Pause();
    }

    public void UnPause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (sounds == null)
        {
            return;
        }
        s.source.UnPause();
    }
}
