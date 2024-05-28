using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;
    
    public Sound[] musicSounds;
    public Sound[] ambienceSounds;
    public Sound[] sfxSounds;
    public AudioSource musicSource, ambienceSource, sfxSource;
    public int currentMusic;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    private void Start() {
        PlayAmbience("ambience");
    }

    public void PlayMusic(int name) {
        if(currentMusic == name) return;
        
        Sound s = musicSounds[name];
        currentMusic = name;
        musicSource.loop = true;
        musicSource.clip = s.clip;
        musicSource.Play();
    }

    public void PlayAmbience(string name) {
        Sound s = Array.Find(ambienceSounds, x => x.name == name);
        ambienceSource.loop = true;
        ambienceSource.clip = s.clip;
        ambienceSource.volume = 0.1f;
        ambienceSource.Play();
    }

    public void PlaySFX(string name, bool loop) {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (loop) {
            sfxSource.loop = true;
            sfxSource.clip = s.clip;
            sfxSource.Play();
        } else
            sfxSource.PlayOneShot(s.clip);
    }
    
}