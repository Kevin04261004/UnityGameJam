using System;
using System.Collections.Generic;
using DYLib;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public enum EMovementType
    {
        Platformer,
        Static,
        Lamp,
        Run,
        
    }
    [SerializeField] private EMovementType curType = EMovementType.Platformer;

    [SerializeField] private SerializableDictionary<EMovementType, BasePlayer> _movementHandler =
        new SerializableDictionary<EMovementType, BasePlayer>();
    public EMovementType CurType
    {
        get => curType;
        set
        {
            _movementHandler[CurType]._movement.FreezeCharacter();
            _movementHandler[CurType].Deactivate();
            curType = value;
            _movementHandler[CurType].Activate();
        }
    }
    
    #region ForDebug

    [ContextMenu("Lamp")]
    private void ChangeToLamp()
    {
        CurType = EMovementType.Lamp;
    }

    [ContextMenu("PlatForm")]
    private void ChangeToPlatform()
    {
        CurType = EMovementType.Platformer;
    }
    [ContextMenu("Static")]
    private void ChangeToStatic()
    {
        CurType = EMovementType.Static;
    }
    [ContextMenu("Runner")]
    private void ChangeToRunner()
    {
        CurType = EMovementType.Run;
    }

    #endregion
    private void Start()
    {
        Init();
    }

    public void SpawnToPoint(Vector3 position)
    {
        // TODO: rigidbody velocity 0으로.
        transform.position = position;
    }
    
    private void Init()
    {
        CurType = curType;
    }
    private void Update()
    {
        _movementHandler[CurType].HandleMovement();
    }

    private void FixedUpdate()
    {
        _movementHandler[CurType].HandlePhysics();
    }
}
