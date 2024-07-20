using System;
using System.Threading;
using UnityEngine;

public class Slime_Boss : Abstract_Enemy ,IDamageable
{
    enum SlimeState
    {
        Idle = 0,
        Attack,
        Shoot,
        Die,
    }
    
    private PlayerStats player;
    [SerializeField] private SlimeState state = SlimeState.Idle;

    

    [SerializeField] private float AttackCooldown = 1f;
    [SerializeField] private float ShootDistance = 4f;
    private float attackTimer;

    private bool isActing = false;

    private Vector2 playerPos;
    private float xMoveValue;
    [SerializeField]private float speed = 2f;
    private Rigidbody2D rb;
    
    private void Start()
    {
        player = FindFirstObjectByType<PlayerStats>();
        
    }

    private void FixedUpdate()
    {
        TimerUpdate();
        
        if (xMoveValue != 0 )
            rb.AddForceX(xMoveValue*Time.fixedDeltaTime);
        
    }

    private void Update()
    {
        switch (state)
        {
            case SlimeState.Idle:
                Idle();
                break;
            case SlimeState.Attack:
                Attack();
                break;
            case SlimeState.Shoot:
                if (!isActing)
                    Shoot();
                break;
            case SlimeState.Die:
                Die();
                break;
            default:
                Debug.LogAssertion($"{this.gameObject.name}::{this.name}::Wrong Condition!!");
                break;
        }
        
        
        
    }

    #region Movements

    protected void Attack()
    {
        if (isActing)
        {
            if (Mathf.Abs(player.transform.position.x - this.transform.position.x) < 0.2f)
            {
                state = SlimeState.Idle;
                isActing = false;
                xMoveValue = 0f;
                return;
            }

            transform.Translate(Vector2.right * (xMoveValue*Time.deltaTime));
            
        }
        else
        {
            float f = (player.transform.position.x - this.transform.position.x);
            float floatTime = f * ((f > 0 ? 1 : -1))/speed;
            
            xMoveValue = (f > 0 ? 1 : -1) * speed;
            
            rb.AddForceY(rb.mass * floatTime,ForceMode2D.Impulse);
            
            attackTimer = AttackCooldown;
            isActing = true;   
        }
        
        
        
    }
    
    protected void Shoot()
    {
        isActing = true;
        
        
        state = SlimeState.Idle;
    }
    protected void Idle()
    {
        
        if (state != SlimeState.Die  && attackTimer <= 0)
        {
            if ((player.transform.position - this.transform.position).magnitude > ShootDistance)
            {
                state = SlimeState.Shoot;
            }
            else
            {
                state = SlimeState.Attack;
            }

        }
        
        isActing = false;
    }

    #endregion
    
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

    private void TimerUpdate()
    {
        if (attackTimer >= 0)
            attackTimer -= Time.fixedDeltaTime;
    }
    
}
