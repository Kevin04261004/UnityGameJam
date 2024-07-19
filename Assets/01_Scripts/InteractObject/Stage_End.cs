using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_End : MonoBehaviour , IInteractableObject
{
    enum stageEnum
    {
        Stage1,
        Stage2,
        Stage3,
        Stage4,
    }

    [SerializeField] private stageEnum targetStage;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }
        
        if (other.GetComponent<PlayerStats>() != null)
        {
            Interact();
        }
        
    }

    public void Interact()
    {
        switch (targetStage)
        {
            case stageEnum.Stage1:
                SceneHandler.Instance.LoadSceneWithFade(SceneHandler.Stage1);
                break;
            case stageEnum.Stage2:
                SceneHandler.Instance.LoadSceneWithFade(SceneHandler.Stage2);
                break;
            case stageEnum.Stage3:
                SceneHandler.Instance.LoadSceneWithFade(SceneHandler.Stage3);
                break;
            case stageEnum.Stage4:
                SceneHandler.Instance.LoadSceneWithFade(SceneHandler.Stage4);
                break;
            default:
                Debug.LogWarning("Scene Missing");
                break;
        }   
    }
    
}
