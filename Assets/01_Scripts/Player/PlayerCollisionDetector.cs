using System;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    private CapsuleCollider2D _collider;
    private Vector3 _groundCheckPos;
    private Vector3 _forwardCheckPos;
    private Vector2 _boxSize;
    private Vector2 _boxStartPos;
    public bool IsWall { get; private set; }
    private Vector2 _overlapSize = new Vector2(0.4f, 0.4f);
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private GetDetectedColliderArray _wallCheck;
    
    public bool Grounded { get; set; }
    private void Awake()
    {
        TryGetComponent(out _collider);
        _groundCheckPos = new Vector3(0, _collider.size.y / 2);
        _forwardCheckPos = new Vector3(_collider.size.x, 0);
        _boxSize = new Vector2(_collider.size.x, _collider.size.y / 0.75f);
    }

    public void CheckOnGround()
    {
        Grounded = Physics2D.OverlapCapsule(transform.position - _groundCheckPos, _overlapSize, CapsuleDirection2D.Horizontal, 0, _groundMask);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        DrawCapsuleGizmo(transform.position - _groundCheckPos, _overlapSize, CapsuleDirection2D.Horizontal);
    }
    private void DrawCapsuleGizmo(Vector2 position, Vector2 size, CapsuleDirection2D direction)
    {
        float radius = size.y / 2;
        Vector2 top = position + Vector2.up * (size.y / 2 - radius);
        Vector2 bottom = position - Vector2.up * (size.y / 2 - radius);

        Gizmos.DrawWireSphere(top, radius);
        Gizmos.DrawWireSphere(bottom, radius);

        if (direction == CapsuleDirection2D.Horizontal)
        {
            Gizmos.DrawLine(top + Vector2.left * radius, bottom + Vector2.left * radius);
            Gizmos.DrawLine(top + Vector2.right * radius, bottom + Vector2.right * radius);
        }
        else
        {
            Gizmos.DrawLine(top + Vector2.up * radius, bottom + Vector2.up * radius);
            Gizmos.DrawLine(top + Vector2.down * radius, bottom + Vector2.down * radius);
        }
    }
    public void CheckForward(PlayerInputHandler.EMoveDir moveDir)
    {
        
    }

    public void CheckWall()
    {
        IsWall = _wallCheck.DetectedColliders.Count >= 1;
    }

    public void CheckInterationObject()
    {
        // TODO: 배열들 전부 돌면서 상호작용 있으면 IInteratable 호출.
        
        
    }

}
