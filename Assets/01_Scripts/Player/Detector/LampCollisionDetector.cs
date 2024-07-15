using System;
using UnityEngine;

public class LampCollisionDetector : BaseCollisionDetector
{
    [field: SerializeField] public CircleCollider2D _collider { get; private set; }
    public override PlayerHandler.EMovementType MovementType { get; protected set; } = PlayerHandler.EMovementType.Lamp;
    private void Awake()
    {
        _groundCheckPos = new Vector3(0, _collider.radius);
    }

}