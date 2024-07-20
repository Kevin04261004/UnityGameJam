using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Obstacle : Abstract_Enemy
{
    
    private void Awake()
    {
        this.GetComponent<Collider2D>().isTrigger = true;
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
        this.gameObject.SetActive(false);
    }

    public override void TakeDamage(int _damage)
    {
        HP -= _damage;
    }
    
}
