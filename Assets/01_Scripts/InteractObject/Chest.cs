using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class Chest : MonoBehaviour, IInteractableObject
{
    public Animator animator;
    public UnityEvent OnOpen;
    public AudioClip chestOpenClip;
    public bool IsOpened
    {
        get { return isOpened; }
        set
        {
            isOpened = value;
            animator.SetBool("IsOpened", isOpened);
        }
    }
    private bool isOpened;

    public void Open()
    {
        IsOpened = true;
    }

    public void Close()
    {
        IsOpened = false;
    }

    [ContextMenu("Interact")]
    public void Interact()
    {
        if (!IsOpened)
        {
            Open();
            StartCoroutine(EndGameRoutine());
            SoundManager.Instance.PlayOneShot(chestOpenClip);
        }
    }

    private IEnumerator EndGameRoutine()
    {
        OnOpen.Invoke();

        SceneHandler.TryGetObjectFromScene(SceneHandler.CharacterScene, out Light2D globalLight);
        globalLight.intensity = 1f;

        float fadeDuration = 1f;
        float elapsedTime = 0.0f;
        float temp = 1f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            temp = Mathf.Clamp01(elapsedTime / fadeDuration);
            globalLight.intensity = temp;
            yield return null;
        }
        
        yield return new WaitForSeconds(2f);
        SceneHandler.Instance.LoadSceneWithFade(SceneHandler.EndScene);
    }

    public void LUTSet()
    {
        StartCoroutine(TestRoutine());
    }

    private IEnumerator TestRoutine()
    {
        if (GameManager.Instance.GlobalVolume.profile.TryGet<ColorLookup>(out ColorLookup lookUp))
        {
            float duration = 1f;
            float elapsedTime = 0.0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                lookUp.contribution.value = Mathf.Lerp(1f, 0f, elapsedTime / duration);
                yield return null;
            }

            // Ensure the final value is set
            lookUp.contribution.value = 0f;
        }
    }
}