using System;
using UnityEngine;

public class CapsuleCollisionDetector : BaseCollisionDetector
{
    [field: SerializeField] public CapsuleCollider2D _collider { get; private set; }

    private void Awake()
    {
        _groundCheckPos = new Vector3(0, _collider.size.y / 2);
    }
}