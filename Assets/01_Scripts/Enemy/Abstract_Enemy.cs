using UnityEngine;

public abstract class Abstract_Enemy : MonoBehaviour, IDamageable
{
    [Header("Parameters")]
    protected int _hp;

    public int HP
    {
        get
        {
            return _hp;
        }
        private set
        {
            if (_hp - value <= 0)
            {
                Die();
                _hp = 0;
            }
            _hp = value;
        }
        
    }

    protected int _damage;

    public int Damage
    {
        get
        {
            return _damage;
        }
        private set
        {
            if (_damage < 0)
                _damage = -_damage;
            _damage = value;
        }
        
    }

    [field:SerializeField]protected LayerMask target_layer;
    
    
    abstract protected void Die();

    public virtual void TakeDamage(int damage)
    {
        _hp -= damage;
    }
    
}
