using UnityEngine;

public class LampAnimatorSignalRecevier : MonoBehaviour
{
    [SerializeField] private AudioClip _landSound;
    [SerializeField] private AudioClip[] _walkSound;

    public void PlayLandSound()
    {
        SoundManager.Instance.PlayOneShot(_landSound);
    }

    public void PlayFootStep()
    {
        SoundManager.Instance.PlayOneShot(_walkSound[Random.Range(0, _walkSound.Length)]);
    }
}
