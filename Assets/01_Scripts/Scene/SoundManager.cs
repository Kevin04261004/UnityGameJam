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

    [SerializeField] private SoundDataSO _soundDataSO;
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

        _soundDataSO.OnLoadDataSuccess += SetVolumes;
        MusicMasterSlider.onValueChanged.AddListener(SetMasterVolume);
        MusicBGMSlider.onValueChanged.AddListener(SetBGMVolume);
        MusicSFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SaveSoundVolumes()
    {
        _soundDataSO.SaveData();
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
    
    private void SetVolumes()
    {
        float masterVolume = _soundDataSO.GetVolume("Master");
        float BGMVolume = _soundDataSO.GetVolume("BGM");
        float SFXVolume = _soundDataSO.GetVolume("SFX");

        MusicMasterSlider.value = masterVolume;
        MusicBGMSlider.value = BGMVolume;
        MusicSFXSlider.value = SFXVolume;
    }
    
    public void SetMasterVolume(float volume)
    {
        if (volume == 0)
        {
            AudioMixer.SetFloat("Master", 0);
        }
        AudioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        _soundDataSO.SetVolume("Master", volume);
    }
    
    public void SetBGMVolume(float volume)
    {
        if (volume == 0)
        {
            AudioMixer.SetFloat("BGM", 0);
        }
        AudioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        _soundDataSO.SetVolume("BGM", volume);
    }
 
    public void SetSFXVolume(float volume)
    {
        if (volume == 0)
        {
            AudioMixer.SetFloat("SFX", 0);
        }
        AudioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        _soundDataSO.SetVolume("SFX", volume);
    }
}
