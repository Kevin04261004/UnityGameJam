using UnityEngine;

public class PlatformerPlayer : BasePlayer
{
    [SerializeField] private PlatformMovementData _platformMovementDataSO;
    public override void HandleMovement()
    {
        base.HandleMovement();
        
        if (_inputHandler.JumpKeyDown && _detector.Grounded)
        {
            _movement.Jump(_platformMovementDataSO.JumpStrength);
            _animator.SetTrigger(EAnimationKeys.Jump.ToString());
        }
        
        if (_inputHandler.InteractKeyDown && _detector.CurInteractableObject != null)
        {
            _detector.CurInteractableObject.Interact();
        }
        
        /* animator Update */
        _animator.SetBool(EAnimationKeys.Grounded.ToString(), _detector.Grounded);
    }

    public override void HandlePhysics()
    {
        base.HandlePhysics();
        
        float speed = _inputHandler.SprintKeyPress
            ? _platformMovementDataSO.SprintSpeed
            : _platformMovementDataSO.WalkSpeed;
        if (_inputHandler.MoveDir == Vector2.zero)
        {
            speed = 0;
        }
        if (_detector.IsWall)
        {
            speed = 0;
        }
        _movement.Move(_inputHandler.MoveDir, speed);
        _animator.SetFloat(EAnimationKeys.Speed.ToString(), speed);
    }
}