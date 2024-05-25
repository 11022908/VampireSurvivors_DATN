using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    public Sprite[] musicImage, sfxImage;
    public Button musicButton, sfxButton;
    private int currentIndexMusic = 0, currentIndexSfx = 0;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    private void Start()
    {
        PlayMusic("background");
        musicButton.onClick.AddListener(() => ChangeImage(this.musicButton, currentIndexMusic, musicImage));
        sfxButton.onClick.AddListener(() => ChangeImage(this.sfxButton, currentIndexSfx, sfxImage));
        
    }
    private void ChangeImage(Button btn, int currentIndex, Sprite[] sprites)
    {
        currentIndex = (currentIndex + 1) % sprites.Length;
        btn.GetComponent<Image>().sprite = sprites[currentIndex];
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if(s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}