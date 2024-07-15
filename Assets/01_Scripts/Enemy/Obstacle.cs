using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Obstacle : Abstract_Enemy
{
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.layer.Equals(target_layer.value))
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

    public override void TakeDamage(int damage)
    {
        return;
    }
    
}
