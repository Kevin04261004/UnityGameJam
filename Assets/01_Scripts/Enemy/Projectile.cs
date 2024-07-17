using UnityEngine;

public class Projectile : MonoBehaviour
{
    [field: SerializeField] private int damage;
    [SerializeField] protected string targetTag = "Player";
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 아닐 시 , 리턴 
        if (other.gameObject.tag != targetTag)
        {
            return;
        }

        
        IDamageable damageable;

        if (other.gameObject.TryGetComponent(out damageable))
        {
            damageable.TakeDamage(this.damage);
        }

        Destroy(this.gameObject);
        
    }
}
