using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Falling_Object : MonoBehaviour
{
    [SerializeField] private float fallSpeed;
    private float currentSpeed = 0;

    [SerializeField] private List<AudioClip> impactAudioClips;
    private AudioSource _audioSource;
    
    public float FallSpeed
    {
        get => fallSpeed;
        set
        {
            fallSpeed = value;
            currentSpeed = fallSpeed * Time.fixedDeltaTime;
        }
    }
    
   
    private bool _isFall;

    private void Awake()
    {
        currentSpeed = fallSpeed * Time.fixedDeltaTime;
        _audioSource = GetComponent<AudioSource>();
        
    }

    public bool IsFall
    {
        get => _isFall;
        set => _isFall = value;
    }
    

    private void FixedUpdate()
    {
        if (_isFall)
        {
            transform.Translate(Vector3.down*currentSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
        {
            _isFall = false;
            _audioSource.PlayOneShot(impactAudioClips[Random.Range(0, impactAudioClips.Count)]);
            Destroy(this);
        }

        if (other.gameObject.TryGetComponent<PlayerStats>(out PlayerStats player))
        {
            Vector2 dirVec =  other.transform.position - this.transform.position;
            dirVec.Normalize();

            float dot = Vector2.Dot(dirVec, Vector2.down);
            if (dot >= 0)
            {
                //Debug.Log("GameOver!");
                player.TakeDamage(100);
            }
            
        }

    }
    
}
