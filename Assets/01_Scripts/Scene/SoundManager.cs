using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private List<AudioSource> audioSourceList = new List<AudioSource>(5);
    private int startCount = 5;
    
    [SerializeField] private AudioMixer AudioMixer;
    [SerializeField] private AudioMixerGroup SFXGroup;
    [SerializeField] private Slider MusicMasterSlider;
    [SerializeField] private Slider MusicBGMSlider;
    [SerializeField] private Slider MusicSFXSlider;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        for (int i = 0; i < startCount; ++i)
        {
            AddAudioSource();
        }
        MusicMasterSlider.onValueChanged.AddListener(SetMasterVolume);
        MusicBGMSlider.onValueChanged.AddListener(SetMusicVolume);
        MusicSFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void PlayOneShot(AudioClip clip)
    {
        AudioSource audioSource = GetAudioSource();
        audioSource.PlayOneShot(clip);
    }

    private AudioSource AddAudioSource()
    {
        AudioSource audioSource = transform.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.outputAudioMixerGroup = SFXGroup;
        audioSource.playOnAwake = false;
        audioSourceList.Add(audioSource);
        return audioSource;
    }

    private AudioSource GetAudioSource()
    {
        foreach (var audioSource in audioSourceList)
        {
            if (!audioSource.isPlaying)
            {
                return audioSource;
            }
        }

        return AddAudioSource();
    }
    
    public void SetMasterVolume(float volume)
    {
        AudioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }
 
    public void SetMusicVolume(float volume)
    {
        AudioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }
 
    public void SetSFXVolume(float volume)
    {
        AudioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
}
