using System;
using UnityEngine;

public class Accelator : MonoBehaviour, IInteractableObject
{
    [Range(0.1f,float.MaxValue)]
    [SerializeField] private float accelarSpeed = 1f;
    private PlayerHandler player;
    
    public void Interact()
    {
        // TODO -> 플레이어 가속 코드 필요 
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerHandler>(out player))
        {
            Interact();    
        }
    }
}
