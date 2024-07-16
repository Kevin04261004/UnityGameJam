using System;
using UnityEngine;

public class PlatformerMovement : BaseMovement
{
    private void Awake()
    {
        TryGetComponent(out _rigid);
    }
    public override void Move(Vector2 moveDir, float speed)
    {
        float x = moveDir.x * speed;
        _rigid.velocity = new Vector2(x, _rigid.velocity.y);
        FlipGO((int)moveDir.x);
    }
    public override void Jump(float strength)
    {
        _rigid.velocity = new Vector2(_rigid.velocity.x, strength);
    }
}
