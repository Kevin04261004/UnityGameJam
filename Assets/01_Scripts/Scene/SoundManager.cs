using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private List<AudioSource> audioSourceList = new List<AudioSource>(5);
    private int startCount = 5;
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
}
