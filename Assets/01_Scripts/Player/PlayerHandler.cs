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

    [SerializeField] private SerializableDictionary<EMovementType, BaseMovementHandler> _movementHandler =
        new SerializableDictionary<EMovementType, BaseMovementHandler>();
    public EMovementType CurType
    {
        get => curType;
        set
        {
            _movementHandler[CurType].Deactivate();
            curType = value;
            _movementHandler[CurType].Activate();
        }
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
    private void Awake()
    {
        Init();
    }

    public void SpawnToPoint(Vector3 position)
    {
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
