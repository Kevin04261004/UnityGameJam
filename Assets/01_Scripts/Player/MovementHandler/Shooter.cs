using UnityEngine;
using UnityEngine.Serialization;

public class Shooter : BasePlayer
{
    public enum EState
    {
        None,
        Charging,
    }

    private EState state;
    public EState State
    {
        set
        {
            if (state == value)
            {
                return;
            }

            if (value == EState.Charging)
            {
                Time.timeScale = 0.7f;
                
                // TODO: 카메라 프로세싱 및 이펙트 효과 적용.
            }
            else
            {
                Time.timeScale = 1f;
                
                // TODO: 카메라 프로세싱 및 이펙트 제거.
            }
        }
    }

    [SerializeField] private Note note;

    public override void HandleMovement()
    {
        base.HandleMovement();
        /* animator Update */
        _animator.SetBool(EAnimationKeys.Grounded.ToString(), _detector.Grounded);

        if (_inputHandler.rightMouseButtonPress && note.CanShoot)
        {
            State = EState.Charging;
            if (_inputHandler.leftMouseButtonUp)
            {
                State = EState.None;

                Vector3 pos = transform.position;
                Vector2 dir = _inputHandler.mouseWorldPosition - (Vector2)pos;
                note.Shoot(dir);
            }
        }
        else
        {
            State = EState.None;
        }

    }

    public override void HandlePhysics()
    {
        base.HandlePhysics();
    }
}