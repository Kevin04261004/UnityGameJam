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
        Debug.DrawRay(transform.position - _groundCheckPos, Vector2.down, Color.green, 0.1f);
        Grounded = Physics2D.Raycast(transform.position - _groundCheckPos, Vector2.down, 0.1f, _groundMask);
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
