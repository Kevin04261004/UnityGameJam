using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour , IDamageable
{

    #region Members 
    [Header("Init Stats")] [SerializeField]
    protected int initHp;
       
    
    [Header("Current Stats")]
    [SerializeField] protected int hp;
    public virtual int Hp
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
    [SerializeField] protected int damage;
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

    private float changedSpeed;

    public float ChangedSpeed
    {
        get => changedSpeed;
        set => changedSpeed = value;
    }
    
    #endregion

    #region Unity Event Functions
    private void Awake()
    {
        Hp = initHp;
    }

    #endregion

    #region Methods
    public void TakeDamage(int _damage)
    {
        Hp -= _damage;
    }

    private void Die()
    {
        //Todo : add game over Function
        
    }
    
    #endregion 
    
}
