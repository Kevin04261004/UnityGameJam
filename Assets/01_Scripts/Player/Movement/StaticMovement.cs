using System;
using UnityEngine;

public class StaticMovement : BaseMovement
{
    private void Awake()
    {
        TryGetComponent(out _rigid);
    }

    public override void Move(Vector2 moveDir, float speed)
    {

    }

    public override void Jump(float strength)
    {
        
    }
}
