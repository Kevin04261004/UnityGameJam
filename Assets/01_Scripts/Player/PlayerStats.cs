using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour , IDamageable
{

    #region Members 
    [Header("Init Stats")] [SerializeField]
    protected int initHp;
    [SerializeField] protected float attackCooldown = 0.2f;
    
    [Header("Current Stats")]
    [SerializeField] protected int hp;

    [SerializeField] public float attackAfterDelay = 0.2f;
    public virtual int Hp
    {
        get => hp;
        private set
        {
            if (value <= 0)
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
    private float attackTimer;

    [field: SerializeField] public bool canAttack { get;  set; }

    [SerializeField] protected TrailRenderer _trailRenderer;
    
    #endregion

    #region Unity Event Functions    
    

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
        GameManager.Instance.SetActiveGameOverUI(true);
        if (this.TryGetComponent<RunNActionPlayer>(out RunNActionPlayer player))
        {
            player.canMove = false;
        }
        
    }
    
    public void Acclerate(float speed ,float  accelTime){
        if (ChangedSpeed < speed)
        {
            ChangedSpeed = speed;
        }

        acclerateTimer = accelTime;
        _trailRenderer.enabled = true;

    }
    private void UpdateTimer()
    {
        if (acclerateTimer >= 0)
            acclerateTimer -= Time.fixedDeltaTime;
        else
        {
            _trailRenderer.enabled = false; 
            ChangedSpeed = 0;
        }
            

        if (attackTimer >= 0)
            attackTimer -= Time.fixedDeltaTime;
        else
            canAttack = true;


    }

    public void InitStats()
    {
        Hp = initHp;
    }
    
    #endregion 
    
}
