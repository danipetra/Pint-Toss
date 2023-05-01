 using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;

using System;


public class AudioManager : MonoBehaviour
{
    /* Static variable used to save the singleton instance */
    private static AudioManager instance;

    public Sound[] sounds;
    public float sceneTransitionTime = 0.4f;

    /* Mixer needed after edit its settings via options menu and PlayerPrefs */
    //public AudioMixerGroup audioMixerGroup;
    //public AudioMixer audioMixer;
    //public OptionsLoader optionsLoader;

     private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        //audioMixer.SetFloat("volume", 100f);
        
        foreach (Sound sound in sounds)
        {
            InitSound(sound);
        }

        StartCoroutine(PlayTheme("Main Theme"));
    }

    private void InitSound(Sound sound){
        sound.source = gameObject.AddComponent<AudioSource>();
        sound.source.clip = sound.clip;
        sound.source.volume = sound.volume;
        sound.source.pitch = sound.pitch;
        sound.source.loop = sound.loop;
        //sound.source.outputAudioMixerGroup = audioMixerGroup;
    }

    IEnumerator PlayTheme(string themeName)
    {
        yield return new WaitForSeconds(sceneTransitionTime);
        Play(themeName);
    }

    public void Update()
    {
        if(SceneManager.GetActiveScene().name == "Reward")
        {
            Pause("Main Theme");
            PlayTheme("Reward Theme");
        }else 
        {
            Pause("Reward Theme");
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
