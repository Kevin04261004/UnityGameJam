using System;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    private CapsuleCollider2D _collider;
    private Vector3 _groundCheckPos;
    private Vector3 _forwardCheckPos;
    private Vector2 _boxSize;
    private Vector2 _boxStartPos;
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private Collider2D[] _forwardObjects;
    
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
        // TODO: 앞부분 체크해서 오브젝트 배열에 넣어두기.
        switch (moveDir)
        {
            case PlayerInputHandler.EMoveDir.Left:
                _boxStartPos = transform.position + _forwardCheckPos;
                break;
            case PlayerInputHandler.EMoveDir.None:
                break;
            case PlayerInputHandler.EMoveDir.Right:
                _boxStartPos = transform.position - _forwardCheckPos;
                break;
            default:
                Debug.Assert(false, "add case");
                break;
        }
        _forwardObjects = Physics2D.OverlapBoxAll(_boxStartPos, _boxSize, 0);
    }
    
}
