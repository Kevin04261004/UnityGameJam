using Unity.Cinemachine;
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
        }
    }

    [SerializeField] private Note note;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform playerLookCamera;
    private static readonly string BounceAbleWall = "BounceAbleWall";
    public override void HandleMovement()
    {
        base.HandleMovement();
        _inputHandler.SetPlayerLookCamOnInput();

        if (_inputHandler.rightMouseButtonPress && note.CanShoot)
        {
            State = EState.Charging;
            if (transform.position.x <= _inputHandler.mouseWorldPosition.x)
            {
                _movement.FlipGO(1);
            }
            else
            {
                _movement.FlipGO(-1);
            }

            Vector2 shootDir = _inputHandler.mouseWorldPosition - (Vector2)transform.position;
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, shootDir, float.MaxValue);
            RaycastHit2D? wallHit = null;
            foreach (var hit in hits)
            {
                if (!hit.transform.CompareTag(BounceAbleWall))
                {
                    continue;
                }

                wallHit = hit;
                break;
            }

            if (wallHit != null)
            {
                SetLineRenderer(transform.position, (Vector3)wallHit?.point);
            }
            else
            {
                SetLineRenderer(transform.position, transform.position);
            }
            
            if (_inputHandler.leftMouseButtonUp)
            {
                State = EState.None;

                Vector3 pos = transform.position;
                Vector2 dir = _inputHandler.mouseWorldPosition - (Vector2)pos;
                note.Shoot(dir);
                SetLineRenderer(transform.position, transform.position);
                _animator.SetBool("idle", false);
            }
        }
        else
        {
            if (note.CanShoot)
            {
                _animator.SetBool("idle", true);
            }
            State = EState.None;
            SetLineRenderer(transform.position, transform.position);
        }

        if (_inputHandler.PlayerLookCamOnKeyDown)
        {
            playerLookCamera.gameObject.SetActive(playerLookCamera.gameObject.activeSelf ? false : true);
        }
    }

    private void SetLineRenderer(Vector3 first, Vector3 second)
    {
        lineRenderer.SetPosition(0, first);
        lineRenderer.SetPosition(1, second);
    }
    public override void HandlePhysics()
    {
        base.HandlePhysics();
    }
}