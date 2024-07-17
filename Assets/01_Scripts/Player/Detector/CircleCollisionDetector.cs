using System;
using UnityEngine;

public class CircleCollisionDetector : BaseCollisionDetector
{
    [field: SerializeField] public CircleCollider2D _collider { get; private set; }
    private void Awake()
    {
        _groundCheckPos = new Vector3(0, _collider.radius);
    }

}