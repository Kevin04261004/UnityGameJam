using System;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;
    [field: SerializeField] public AudioSource _audioSource { get; set; }

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
        if (_audioSource.clip != null && _audioSource.clip == clip)
        {
            return;
        }
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
