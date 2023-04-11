using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource _musicSource, _effectsSource;

    [SerializeField] private AudioClip _menuMusic, _gameMusic;

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

        if(currentScene.name == "TeamProject")
        {
            _musicSource.clip = _gameMusic;
        }
        else
        {
            _musicSource.clip = _menuMusic;
        }

    }

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
