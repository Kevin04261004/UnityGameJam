using UnityEngine;

public class RunNActionCollisionDetector : BaseCollisionDetector
{
    [field: SerializeField] public CapsuleCollider2D _collider { get; private set; }
    public IDamageable CurDamageableObject { get; protected set; }
    [SerializeField] protected GetDetectedColliderArray _enemyCheck;
    
    public virtual void CheckDamageableObject()
    {
        CurInteractableObject = null;
        foreach (var col in _enemyCheck.DetectedColliders)
        {
            if (col.TryGetComponent(out IDamageable damageableObject))
            {
                CurDamageableObject = damageableObject;
                break;
            }
        }

    }
    
    private void Awake()
    {
        _groundCheckPos = new Vector3(0, _collider.size.y / 2);
    }
    
    public override void CheckOnGround()
    {
        Grounded = Physics2D.OverlapCapsule(transform.position - _groundCheckPos, OVERLAP_CIRCLE_SIZE,
            CapsuleDirection2D.Horizontal, 0, _groundMask);
    }
    
}
