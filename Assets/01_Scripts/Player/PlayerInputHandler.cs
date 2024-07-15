using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public bool JumpKeyDown { get; set; }
    public bool SprintKeyDown { get; set; }
    public float MoveDirX { get; set; }

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
        MoveDirX = 0;
        if (Input.GetKey(KeyCode.A))
        {
            MoveDirX--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveDirX++;
        }
    }
}
