using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public enum EMovementType
    {
        Platformer,
        Lamp,
    }
    private PlayerInputHandler _inputHandler;
    private Dictionary<EMovementType, BaseMovement> _movement = new Dictionary<EMovementType, BaseMovement>();
    private Dictionary<EMovementType, BaseCollisionDetector> _detector = new Dictionary<EMovementType, BaseCollisionDetector>();
    private Dictionary<EMovementType, Collider2D> _collider = new Dictionary<EMovementType, Collider2D>();
    private Dictionary<EMovementType, GameObject> _mesh = new Dictionary<EMovementType, GameObject>();
    
    [SerializeField] private EMovementType curType = EMovementType.Platformer;
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

    [SerializeField] private Collider2D _platformCollider;
    [SerializeField] private Collider2D _lampCollider;
    [SerializeField] private GameObject _platformMesh;
    [SerializeField] private GameObject _lampMesh;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        TryGetComponent(out _inputHandler);
        
        BaseCollisionDetector[] detectors = GetComponents<BaseCollisionDetector>();
        if (detectors != null)
        {
            foreach (var dectector in detectors)
            {
                Debug.Assert(!_detector.ContainsKey(dectector.MovementType), $"이미 {dectector.MovementType} Key가 존재합니다.");
                _detector.Add(dectector.MovementType, dectector);
            }
        }
        
        BaseMovement[] movements = GetComponents<BaseMovement>();
        if (movements != null)
        {
            foreach (var movement in movements)
            {
                Debug.Assert(!_movement.ContainsKey(movement.MovementType), $"이미 {movement.MovementType} Key가 존재합니다.");
                _movement.Add(movement.MovementType, movement);
            }
        }
        Debug.Assert(_platformMovementDataSO != null);
        Debug.Assert(_lampMovementDataSO != null);
        Debug.Assert(_platformCollider != null);
        Debug.Assert(_lampCollider != null);
        Debug.Assert(_platformMesh != null);
        Debug.Assert(_lampMesh != null);
        
        _collider.Add(EMovementType.Platformer, _platformCollider);
        _collider.Add(EMovementType.Lamp, _lampCollider);
        _mesh.Add(EMovementType.Platformer, _platformMesh);
        _mesh.Add(EMovementType.Lamp, _lampMesh);
        
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

        // _detector.CheckInterationObject();
        
        
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
                return _inputHandler.SprintKeyDown ? _platformMovementDataSO.SprintSpeed : _platformMovementDataSO.WalkSpeed;
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
