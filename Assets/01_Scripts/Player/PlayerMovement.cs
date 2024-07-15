using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigid;
    private static readonly Vector3 leftFlip = new Vector3(-1, 1, 1);
    private static readonly Vector3 rightFlip = new Vector3(1, 1, 1);
    
    private void Awake()
    {
        TryGetComponent(out _rigid);
    }
    
    public void Move(PlayerInputHandler.EMoveDir moveDir, float speed)
    {
        float x = (int)moveDir * speed;
        _rigid.velocity = new Vector2(x, _rigid.velocity.y);
        FlipGO(moveDir);
    }
    private void FlipGO(PlayerInputHandler.EMoveDir moveDir)
    {
        switch (moveDir)
        {
            case PlayerInputHandler.EMoveDir.Left:
                transform.localScale = leftFlip;
                break;
            case PlayerInputHandler.EMoveDir.None:
                break;
            case PlayerInputHandler.EMoveDir.Right:
                transform.localScale = rightFlip;
                break;
            default:
                Debug.Assert(false, "Add case");
                break;
        }
    }
    public void Jump(float strength)
    {
        _rigid.velocity = new Vector2(_rigid.velocity.x, strength);
    }
}
