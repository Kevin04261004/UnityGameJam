using System;
using UnityEngine;

public class Accelator : MonoBehaviour, IInteractableObject
{
    [Range(0.1f,30f)]
    [SerializeField] private float accelarSpeed = 1f;
    private PlayerStats player;
    
    public void Interact()
    {
        player.ChangedSpeed += accelarSpeed;
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //플레이어가 아닐 시 return 
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        if (other.TryGetComponent<PlayerStats>(out player))
        {
            //Debug.Log($"player Speed : {player.ChangedSpeed} , After : {player.ChangedSpeed + this.accelarSpeed}");
            Interact();    
        }
    }
}
