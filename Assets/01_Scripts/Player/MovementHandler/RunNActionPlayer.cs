using System;
using Unity.VisualScripting;
using UnityEngine;

public class RunNActionPlayer : BasePlayer
{
    [SerializeField] private RunMovementData _runMovementDataSO;
    [SerializeField] private PlayerStats playerStats;
    //[field: SerializeField] private bool canAttack { get; set; }
    
    //[field: SerializeField] public RunNActionCollisionDetector _RunNActionCollisionDetectordetector { get; protected set; }
    //[field: SerializeField] public CapsuleCollisionDetector _detector { get; protected set; }

    [field: SerializeField] public bool canMove { get; set; } = false;
    
    
    
    private void Start()
    {
        if (playerStats == null)
        {
            TryGetComponent<PlayerStats>(out playerStats);
        }
        Debug.Assert(playerStats != null, "playerStats != null");

    }

    public override void HandleMovement()
    {
        if (!canMove)
            return;
        
        base.HandleMovement();
        
        if (_inputHandler.JumpKeyDown && _detector.Grounded)
        {
            _movement.Jump(_runMovementDataSO.JumpStrength);
            _animator.SetTrigger(EAnimationKeys.Jump.ToString());
        }
        
        // interact
        if (_inputHandler.InteractKeyDown && _detector.CurInteractableObject != null)
        {
            _detector.CurInteractableObject.Interact();
        }
        
        /* Attack */
        if (_inputHandler.leftMouseButtonDown && playerStats.canAttack && _detector.Grounded)
        {
            // TODO -> 공격 및 애니메이션 추가
            canMove = false;
            Invoke("ActiveMove",playerStats.attackAfterDelay);
            _animator.SetTrigger(EAnimationKeys.Attack.ToString());
            playerStats.canAttack = false;
            if ( (_detector as RunNActionCollisionDetector).CurDamageableObject != null)
                (_detector as RunNActionCollisionDetector).CurDamageableObject.TakeDamage(_runMovementDataSO.DamagePower);
            
            
        }
        
        /* animator Update */
        _animator.SetBool(EAnimationKeys.Grounded.ToString(), _detector.Grounded);
        
    }

    private void ActiveMove()
    {
        canMove = true;
    }
    
    public override void HandlePhysics()
    {
        if (!canMove)
            return;
        
        base.HandlePhysics();
        (_detector as RunNActionCollisionDetector).CheckDamageableObject();

        float speed = (_inputHandler.SprintKeyPress
            ? _runMovementDataSO.SprintSpeed
            : _runMovementDataSO.WalkSpeed) + playerStats.ChangedSpeed;
        
        if (_inputHandler.MoveDir.x == 0)
        {
            speed = 0;
        }
        if (_detector.IsWall)
        {
            speed = 0;
        }
        
        //Debug.Log($"Speed : {speed}" );
        _movement.Move(_inputHandler.MoveDir, speed);
        _animator.SetFloat(EAnimationKeys.Speed.ToString(), speed);

        
    }

    public override void Activate()
    {
        base.Activate();
        playerStats.InitStats();
        canMove = true;
    }
    
}
