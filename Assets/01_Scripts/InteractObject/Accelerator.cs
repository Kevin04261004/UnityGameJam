using System;
using UnityEngine;

public class Accelator : MonoBehaviour
{
    [Range(0.1f,30f)]
    [SerializeField] private float accelarSpeed = 1f;
    [SerializeField] private float accelarTime = 1f;
    private PlayerStats player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //플레이어가 아닐 시 return 
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        if (other.TryGetComponent<PlayerStats>(out player))
        {
            player.Acclerate(accelarSpeed,accelarTime);
            this.gameObject.SetActive(false);  
        }
        
    }
}
