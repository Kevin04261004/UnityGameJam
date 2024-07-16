using System;
using System.Collections.Generic;
using DYLib;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public enum EMovementType
    {
        Platformer,
        Lamp,
    }
    private PlayerInputHandler _inputHandler;
    [SerializeField] private EMovementType curType = EMovementType.Platformer;

    [SerializeField] private SerializableDictionary<EMovementType, BaseMovement> _movement = new SerializableDictionary<EMovementType, BaseMovement>();
    [SerializeField] private SerializableDictionary<EMovementType, BaseCollisionDetector> _detector = new SerializableDictionary<EMovementType, BaseCollisionDetector>();
    [SerializeField] private SerializableDictionary<EMovementType, Collider2D> _collider = new SerializableDictionary<EMovementType, Collider2D>();
    [SerializeField] private SerializableDictionary<EMovementType, GameObject> _mesh = new SerializableDictionary<EMovementType, GameObject>();
    public EMovementType CurType
    {
        get => curType;
        private set
        {
            _movement[CurType].enabled = false;
            _detector[CurType].enabled = false;
            _collider[CurType].enabled = false;
            _mesh[CurType].SetActive(false);
            curType = value;
            _movement[CurType].enabled = true;
            _detector[CurType].enabled = true;
            _collider[CurType].enabled = true;
            _mesh[CurType].SetActive(true);
        }
    }
    
    [SerializeField] private PlatformMovementData _platformMovementDataSO;
    [SerializeField] private LampMovementData _lampMovementDataSO;

    private void Awake()
    {
        Init();
    }

    #region ForDebug

    [ContextMenu("Lamp")]
    public void ChangeToLamp()
    {
        CurType = EMovementType.Lamp;
    }

    [ContextMenu("PlatForm")]
    public void ChangeToPlatform()
    {
        CurType = EMovementType.Platformer;
    }

    #endregion

    public void SpawnToPoint(Vector3 position)
    {
        transform.position = position;
    }
    
    private void Init()
    {
        TryGetComponent(out _inputHandler);
        
        Debug.Assert(_platformMovementDataSO != null);
        Debug.Assert(_lampMovementDataSO != null);
        
        /* Debug */
        foreach (EMovementType m in Enum.GetValues(typeof(EMovementType)))
        {
            Debug.Assert(_movement.ContainsKey(m));
            Debug.Assert(_detector.ContainsKey(m));
            Debug.Assert(_collider.ContainsKey(m));
            Debug.Assert(_mesh.ContainsKey(m));
        }

        CurType = curType;
    }
    private void Update()
    {
        _inputHandler.GetJumpInput();
        _inputHandler.GetSprintInput();
        _inputHandler.GetMovementInput();
        _inputHandler.GetInteractInput();
        if (_inputHandler.JumpKeyDown && _detector[CurType].Grounded)
        {
            _movement[CurType].Jump(GetJumpStrength());
        }
        
        _detector[CurType].CheckInteractObject();
        if (_inputHandler.InteractKeyDown && _detector[CurType].CurInteractableObject != null)
        {
            _detector[CurType].CurInteractableObject.Interact();
        }
    }

    private void FixedUpdate()
    {
        /* physics */
        _detector[CurType].CheckOnGround();

        /* move */
        float speed = GetSpeed();
        _detector[CurType].CheckWall();
        if (_detector[CurType].IsWall)
        {
            speed = 0;
        }
        _movement[CurType].Move(_inputHandler.MoveDir, speed);
    }

    private float GetSpeed()
    {
        switch (CurType)
        {
            case EMovementType.Platformer:
                return _inputHandler.SprintKeyPress ? _platformMovementDataSO.SprintSpeed : _platformMovementDataSO.WalkSpeed;
            case EMovementType.Lamp:
                return _lampMovementDataSO.Speed;
            default:
                Debug.Assert(false, "Add Case");
                return 0;
        }
    }

    private float GetJumpStrength()
    {
        switch (CurType)
        {
            case EMovementType.Platformer:
                return _platformMovementDataSO.JumpStrength;
            case EMovementType.Lamp:
                return _lampMovementDataSO.JumpStrength;
            default:
                Debug.Assert(false, "Add Case");
                return 0;
        }
    }
}
