using System;
using UnityEngine;

public class LampMovement : BaseMovement
{
    public override PlayerHandler.EMovementType MovementType { get; protected set; } = PlayerHandler.EMovementType.Lamp;

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