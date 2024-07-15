using System;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    private PlayerInputHandler _inputHandler;
    private PlayerMovement _movement;
    private PlayerCollisionDetector _detector;
    [SerializeField] private PlayerMovementData _movementDataSO;

    private void Awake()
    {
        TryGetComponent(out _inputHandler);
        TryGetComponent(out _movement);
        TryGetComponent(out _detector);
        Debug.Assert(_movementDataSO != null);
    }

    private void Update()
    {
        _inputHandler.GetJumpInput();
        _inputHandler.GetSprintInput();
        _inputHandler.GetMovementInput();

        if (_inputHandler.JumpKeyDown) // && CanJump())
        {
            _movement.Jump(_movementDataSO.JumpStrength);
        }
    }

    private void FixedUpdate()
    {
        /* physics */
        _detector.CheckOnGround();
        
        /* move */
        float speed = _inputHandler.SprintKeyDown ? _movementDataSO.SprintSpeed : _movementDataSO.WalkSpeed;
        _movement.Move(_inputHandler.MoveDirX, speed);
    }
}
