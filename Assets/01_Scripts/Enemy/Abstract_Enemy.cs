using UnityEngine;
using UnityEngine.Serialization;

public abstract class Abstract_Enemy : MonoBehaviour, IDamageable
{
    [FormerlySerializedAs("_hp")]
    [Header("Parameters")]
    [SerializeField] protected int hp;

    public virtual int HP
    {
        get => hp;
        private set
        {
            if (hp - value <= 0)
            {
                Die();
                hp = 0;
            }
            hp = value;
        }
        
    }

    [FormerlySerializedAs("_damage")] [SerializeField] protected int damage;

    public virtual int Damage
    {
        get => damage;
        private set
        {
            if (damage < 0)
                damage = -damage;
            damage = value;
        }
        
    }

    [FormerlySerializedAs("target_layer")] [SerializeField] protected string targetTag = "Player";
    
    
    abstract protected void Die();

    public virtual void TakeDamage(int _damage)
    {
        HP -= _damage;
    }
    
}
