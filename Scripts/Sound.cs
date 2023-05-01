using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f,12f)]public float volume;
    [Range(.5f,1.3f)]public float pitch;
    public bool loop;
    [HideInInspector]
    public AudioSource source;
}
