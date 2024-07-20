using UnityEngine;
using UnityEngine.UI;

public class FormChangere : MonoBehaviour,IInteractableObject
{
    [SerializeField] private PlayerHandler.EMovementType targetType;
    
    public void Interact()
    {
        PlayerHandler playerHandler = FindAnyObjectByType<PlayerHandler>();

        if (!playerHandler)
        {
            Debug.LogWarning("Player doesn't exist ");
            return;
        }

        playerHandler.CurType = targetType;


    }
    
}
