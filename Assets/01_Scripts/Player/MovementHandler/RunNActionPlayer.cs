using System;
using Unity.VisualScripting;
using UnityEngine;

public class RunNActionPlayer : BasePlayer
{
    [SerializeField] private RunMovementData _runMovementDataSO;
    [SerializeField] private PlayerStats playerStats;
    [field: SerializeField] private bool canAttack { get; set; }
    [field: SerializeField] public RunNActionCollisionDetector _detector { get; protected set; }
    
    

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
        base.HandleMovement();
        
        if (_inputHandler.JumpKeyDown && _detector.Grounded)
        {
            _movement.Jump(_runMovementDataSO.JumpStrength);
            _animator.SetTrigger(EAnimationKeys.Jump.ToString());
        }
        
        /* Attack */
        if (_inputHandler.leftMouseButtonDown && canAttack && _detector.Grounded)
        {
            // TODO -> 공격 및 애니메이션 추가
            _animator.SetTrigger(EAnimationKeys.Attack.ToString());
            if ( _detector.CurDamageableObject != null)
                _detector.CurDamageableObject.TakeDamage(_runMovementDataSO.DamagePower);
        }
        
        /* animator Update */
        _animator.SetBool(EAnimationKeys.Grounded.ToString(), _detector.Grounded);
    }

    public override void HandlePhysics()
    {
        base.HandlePhysics();
        _detector.CheckDamageableObject();

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
    
    
}
