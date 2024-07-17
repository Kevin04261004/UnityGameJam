using System;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private Camera mainCamera;
    public bool JumpKeyDown { get; private set; }
    public bool SprintKeyPress { get; private set; }
    public Vector2 MoveDir { get; private set; }
    public bool InteractKeyDown { get; private set; }
    public Vector2 mouseScreenPosition { get; private set; }
    public Vector2 mouseWorldPosition { get; private set; }
    public bool leftMouseButtonDown { get; private set; }
    public bool rightMouseButtonDown { get; private set; }
    public bool leftMouseButtonPress { get; private set; }
    public bool rightMouseButtonPress { get; private set; }
    public bool leftMouseButtonUp { get; private set; }
    public bool rightMouseButtonUp { get; private set; }

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void SetJumpInput()
    {
        JumpKeyDown = Input.GetKeyDown(KeyCode.Space);
    }
    public void SetInteractInput()
    {
        InteractKeyDown = Input.GetKeyDown(KeyCode.F);
    }
    public void SetSprintInput()
    {
        SprintKeyPress = Input.GetKey(KeyCode.LeftShift);
    }
    public void SetMouseInput()
    {
        mouseScreenPosition = Input.mousePosition;
        mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        leftMouseButtonDown = Input.GetMouseButtonDown(0);
        rightMouseButtonDown = Input.GetMouseButtonDown(1);
        leftMouseButtonPress = Input.GetMouseButton(0);
        rightMouseButtonPress = Input.GetMouseButton(1);
        leftMouseButtonUp = Input.GetMouseButtonUp(0);
        rightMouseButtonUp = Input.GetMouseButtonUp(1);
    }
    public void SetMovementInput()
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
