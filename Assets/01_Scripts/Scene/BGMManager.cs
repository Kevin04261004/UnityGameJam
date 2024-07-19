using System;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;
    [SerializeField] private AudioSource _audioSource;

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
    }

    public void PlayBGM(AudioClip clip)
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }

        if (_audioSource.clip == clip)
        {
            return;
        }
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
