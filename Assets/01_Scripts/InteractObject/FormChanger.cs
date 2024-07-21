using UnityEngine;
using UnityEngine.UI;

public class FormChangere : MonoBehaviour,IInteractableObject
{
    [SerializeField] private PlayerHandler.EMovementType targetType;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private bool condition;
    [SerializeField] private Stage_End stageEnd;
    public void Interact()
    {
        PlayerHandler playerHandler = FindAnyObjectByType<PlayerHandler>();

        if (!playerHandler)
        {
            Debug.LogWarning("Player doesn't exist ");
            return;
        }
        
        targetObject.SetActive(condition);
        playerHandler.CurType = targetType;
        GetComponent<Collider2D>().enabled = false;

        stageEnd.CanGoNextStage = true;
    }
    
}
