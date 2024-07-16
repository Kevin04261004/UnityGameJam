using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour, IInteractableObject
{
    public Animator animator;
    public UnityEvent OnOpen;
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
        }
    }

    private IEnumerator EndGameRoutine()
    {
        OnOpen.Invoke();
        yield return new WaitForSeconds(3f);
        SceneHandler.Instance.LoadSceneWithFade(SceneHandler.EndScene);
    }
}