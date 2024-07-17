using System;
using UnityEngine;

public abstract class BasePlayer : MonoBehaviour
{
    [field: SerializeField] public BaseMovement _movement { get; protected set; }
    [field: SerializeField] public BaseCollisionDetector _detector { get; protected set; }
    [field: SerializeField] public Collider2D _collider { get; protected set; }
    [field: SerializeField] public GameObject _meshGO { get; protected set; }
    [field: SerializeField] public Animator _animator { get; protected set; }
    protected PlayerInputHandler _inputHandler;

    private void Awake()
    {
        Debug.Assert(_movement != null, "_movement != null");
        Debug.Assert(_detector != null, "_detector != null");
        Debug.Assert(_collider != null, "_collider != null");
        Debug.Assert(_meshGO != null, "_meshGO != null");
        Debug.Assert(_animator != null, "_animator != null");
        TryGetComponent(out _inputHandler);
        Debug.Assert(_inputHandler != null, "_inputHandler != null");

    }

    public void Activate()
    {
        _movement.enabled = true;
        _detector.enabled = true;
        _collider.enabled = true;
        _meshGO.SetActive(true);
    }

    public void Deactivate()
    {
        _movement.enabled = false;
        _detector.enabled = false;
        _collider.enabled = false;
        _meshGO.SetActive(false);
    }

    public virtual void HandleMovement()
    {
        /* Input */
        _inputHandler.SetJumpInput();
        _inputHandler.SetSprintInput();
        _inputHandler.SetMouseInput();
        _inputHandler.SetMovementInput();
        _inputHandler.SetInteractInput();
    }
    public virtual void HandlePhysics()
    {
        /* physics Check */
        _detector.CheckOnGround();
        _detector.CheckWall();
        _detector.CheckInteractObject();
    }
}