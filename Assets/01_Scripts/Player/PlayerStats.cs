using System;
using Unity.VisualScripting;
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
    
    private float acclerateTimer;
    
    #endregion

    #region Unity Event Functions
    private void Awake()
    {
        Hp = initHp;
    }

    private void FixedUpdate()
    {
        UpdateTimer();
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
    
    public void Acclerate(float speed ,float  accelTime){
        if (ChangedSpeed < speed)
        {
            ChangedSpeed = speed;
        }

        acclerateTimer = accelTime;

    }
    private void UpdateTimer()
    {
        if (acclerateTimer >= 0)
            acclerateTimer -= Time.fixedDeltaTime;
        else
            ChangedSpeed = 0;
        
    } 
    
    #endregion 
    
}
