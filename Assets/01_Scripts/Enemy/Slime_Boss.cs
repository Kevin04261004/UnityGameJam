using UnityEngine;

public class Slime_Boss : Abstract_Enemy ,IDamageable
{
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
        return;
    }
}
