using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioSystem : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;

        public AudioClip audioClip;

        [Range(0f, 1f)] public float volume;
        [Range(0.1f, 3f)] public float pitch;
        public bool loop;

        [HideInInspector] public AudioSource source;
    }

    public static AudioSystem instance;
    public Sound[] sounds;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.audioClip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        AudioSystem.instance.PlaySound("Background");
    }

    void Update()
    {
        
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
