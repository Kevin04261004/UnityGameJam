using System;
using UnityEngine;

public abstract class BaseCollisionDetector : MonoBehaviour
{
    protected Vector3 _groundCheckPos;
    public bool IsWall { get; protected set; }
    public bool Grounded { get; protected set; }
    public IInteractableObject CurInteractableObject { get; protected set; }
    
    [SerializeField] protected LayerMask _groundMask;
    [SerializeField] protected GetDetectedColliderArray _wallCheck;
    [SerializeField] protected GetDetectedColliderArray _interactCheck;
    protected static readonly Vector2 OVERLAP_CIRCLE_SIZE = new Vector2(0.4f, 0.4f);

    protected void DrawCapsuleGizmo(Vector2 position, Vector2 size, CapsuleDirection2D direction)
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

    public virtual void CheckOnGround()
    {
        Grounded = Physics2D.OverlapCapsule(transform.position - _groundCheckPos, OVERLAP_CIRCLE_SIZE, CapsuleDirection2D.Horizontal, 0, _groundMask);
    }

    public virtual void CheckInteractObject()
    {
        CurInteractableObject = null;
        foreach (var col in _interactCheck.DetectedColliders)
        {
            if (col.TryGetComponent(out IInteractableObject interactableObject))
            {
                CurInteractableObject = interactableObject;
                break;
            }
        }

    }

    public virtual void CheckWall()
    {
        IsWall = _wallCheck.DetectedColliders.Count >= 1;
    }

    protected virtual void OnDrawGizmos()
    {
        if (!enabled)
        {
            return;
        }
        Gizmos.color = Color.green;
        DrawCapsuleGizmo(transform.position - _groundCheckPos, OVERLAP_CIRCLE_SIZE, CapsuleDirection2D.Horizontal);
    }
}