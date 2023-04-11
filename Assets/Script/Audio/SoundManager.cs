using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
using UnityEngine.Audio;
=======
using UnityEngine.SceneManagement;
>>>>>>> Stashed changes

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public Sound[] sounds;

    [SerializeField] private AudioSource _musicSource, _effectsSource;

    [SerializeField] public AudioClip _menuMusic, _gameMusic;

    Scene currentScene;
    
    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }

    }

<<<<<<< Updated upstream
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
            
        s.source.PlayOneShot(s.clip);
    }


=======
    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();

        if(currentScene.name == "TeamProject")
        {
            _musicSource.clip = _gameMusic;
        }
        else
        {
            _musicSource.clip = _menuMusic;
        }
    }
>>>>>>> Stashed changes
    // Plays a single sound
    public void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void ToggleEffects()
    {
        _effectsSource.mute = !_effectsSource.mute;
    }

    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
    }

    
}
