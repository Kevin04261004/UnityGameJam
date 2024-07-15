using System;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public bool JumpKeyDown { get; private set; }
    public bool SprintKeyPress { get; private set; }
    public Vector2 MoveDir { get; private set; }
    public bool InteractKeyDown { get; private set; }
    public void GetJumpInput()
    {
        JumpKeyDown = Input.GetKeyDown(KeyCode.Space);
    }

    public void GetInteractInput()
    {
        InteractKeyDown = Input.GetKeyDown(KeyCode.F);
    }
    public void GetSprintInput()
    {
        SprintKeyPress = Input.GetKey(KeyCode.LeftShift);
    }
    public void GetMovementInput()
    {
        int x = 0;
        int y = 0;
        if (Input.GetKey(KeyCode.A))
        {
            x--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            x++;
        }
        if (Input.GetKey(KeyCode.W))
        {
            y++;
        }
        if (Input.GetKey(KeyCode.S))
        {
            y--;
        }

        MoveDir = new Vector2(x, y);
    }
}
