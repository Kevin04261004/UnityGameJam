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

    public void CheckWall()
    {
        // TODO: 배열에 벽 레이어가 있으면 플레이어와 벽간의 x축 좌표의 거리를 구하고, 만약 0.1보다 작으면 bIsWall = true 아니면 false; 
        IsWall = false;
    }

    public void CheckInterationObject()
    {
        // TODO: 배열들 전부 돌면서 상호작용 있으면 IInteratable 호출.
        
        
    }

}
