using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f,6f)]
    public float volume;
    [Range(.7f,1.3f)]
    public float pitch;
    public bool loop;
    [HideInInspector]
    public AudioSource source;
}
