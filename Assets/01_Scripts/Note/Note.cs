using System;
using UnityEngine;

public class Note : MonoBehaviour
{
    public enum EState
    {
        Ready,
        Shoot,
        End,
    }

    public EState state { get; set; }
    public Vector2 Dir { get; set; }
    public float Speed { get; set; }
    public float MaxBounceCount { get; set; } = 5;

    public void Update()
    {
        switch (state)
        {
            case EState.Ready:
            case EState.End:
                return;
            case EState.Shoot:
                break;
            default:
                Debug.Assert(false, "Add case");
                return;
        }
    }

    public void FixedUpdate()
    {
        switch (state)
        {
            case EState.Ready:
            case EState.End:
                break;
            case EState.Shoot:
                ShootPhysicsUpdate();
                break;
            default:
                Debug.Assert(false, "Add case");
                return;
        }
    }

    public void Shoot(Vector2 dir)
    {
        if (state == EState.Shoot)
        {
            return;
        }

        state = EState.Shoot;
        this.Dir = dir.normalized;
    }
    
    private void ShootPhysicsUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, 1f);
        if (hit == null)
        {
            return;
        }

        float angle = Vector2.Angle(Dir, hit.normal) * 2;
        
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        
    }
}