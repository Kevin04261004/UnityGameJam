using UnityEngine;

public class RunNActionCollisionDetector : BaseCollisionDetector
{
    [field: SerializeField] public CapsuleCollider2D _collider { get; private set; }
    [field: SerializeField] private LayerMask _platformMask;
    
    private void Awake()
    {
        _groundCheckPos = new Vector3(0, _collider.size.y / 2);
    }
    
    public override void CheckOnGround()
    {
        Grounded = 
            Physics2D.OverlapCapsule(transform.position - _groundCheckPos, OVERLAP_CIRCLE_SIZE, CapsuleDirection2D.Horizontal, 0, _groundMask)
            || Physics2D.OverlapCapsule(transform.position - _groundCheckPos, OVERLAP_CIRCLE_SIZE, CapsuleDirection2D.Horizontal, 0, _platformMask);
        
        
    }
    
}
