using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigid;

    private void Awake()
    {
        TryGetComponent(out _rigid);
    }
    
    public void Move(float moveDirX, float speed)
    {
        float x = moveDirX * speed * Time.deltaTime;
        _rigid.velocity = new Vector2(x, _rigid.velocity.y);
    }

    public void Jump(float strength)
    {
        _rigid.velocity = new Vector2(_rigid.velocity.x, strength * Time.deltaTime);
    }
}
