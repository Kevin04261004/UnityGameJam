using System;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    private CapsuleCollider2D _collider;
    private Vector2 _groundCheckPos;
    [SerializeField] private LayerMask _groundMask;
    
    public bool Grounded { get; set; }
    private void Awake()
    {
        TryGetComponent(out _collider);
        _groundCheckPos = new Vector2(transform.position.x, transform.position.y - (_collider.size.y / 2));
    }

    public void CheckOnGround()
    {
        Grounded = Physics2D.Raycast(_groundCheckPos, Vector2.down, 0.1f, _groundMask);
    }
}
