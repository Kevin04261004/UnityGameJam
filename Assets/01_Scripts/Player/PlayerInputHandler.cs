using System;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public enum EMoveDir
    {
        Left = -1,
        None = 0,
        Right = 1,
    }
    public bool JumpKeyDown { get; private set; }
    public bool SprintKeyDown { get; private set; }
    public EMoveDir MoveDir { get; private set; }
    public void GetJumpInput()
    {
        JumpKeyDown = Input.GetKeyDown(KeyCode.Space);
    }
    public void GetSprintInput()
    {
        SprintKeyDown = Input.GetKey(KeyCode.LeftShift);
    }
    public void GetMovementInput()
    {
        MoveDir = EMoveDir.None;
        if (Input.GetKey(KeyCode.A))
        {
            MoveDir--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveDir++;
        }
    }
}
