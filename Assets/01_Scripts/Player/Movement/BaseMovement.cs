using UnityEngine;

public abstract class BaseMovement : MonoBehaviour
{
    public abstract PlayerHandler.EMovementType MovementType { get; protected set; }
    protected Rigidbody2D _rigid;
    private static readonly Vector3 leftFlip = new Vector3(-1, 1, 1);
    private static readonly Vector3 rightFlip = new Vector3(1, 1, 1);

    public abstract void Move(Vector2 moveDir, float speed);
    protected void FlipGO(int moveDirX)
    {
        switch (moveDirX)
        {
            case -1:
                transform.localScale = leftFlip;
                break;
            case 0:
                break;
            case 1:
                transform.localScale = rightFlip;
                break;
            default:
                Debug.Assert(false, "Add case");
                break;
        }
    }

    public abstract void Jump(float strength);
}
