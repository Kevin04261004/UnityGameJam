using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Collider2D))]
public class Obstacle : Abstract_Enemy
{
    [SerializeField] private List<Sprite> _sprites; 
    private void Awake()
    {
        if (_sprites.Count > 0)
        {
            GetComponent<SpriteRenderer>().sprite = _sprites[Random.Range(0, _sprites.Count)];
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag != targetTag)
        {
            return;
        }

        
        
        IDamageable damageable;

        if (other.gameObject.TryGetComponent(out damageable))
        {
            damageable.TakeDamage(this.Damage);
        }
        
    }

    protected override void Die()
    {
        if (gameObject == null || !gameObject.activeSelf)
        {
            return;
        }
        this.gameObject.SetActive(false);
    }

    public override void TakeDamage(int _damage)
    {
        HP -= _damage;
    }
    
}
