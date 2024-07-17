using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Note : MonoBehaviour
{
    private static readonly string BounceAbleWall = "BounceAbleWall"; 
    public enum EState
    {
        Ready,
        Shoot,
        End,
    }
    public EState State { get; private set; }
    public Vector2 Dir { get; set; }
    [field: SerializeField]public float Speed { get; set; }

    [field: SerializeField] public float MaxBounceCount { get; set; } = 5;
    private Rigidbody2D _rigid;
    [SerializeField] private float curBounceCount = 0;
    [SerializeField] private UnityEvent OnReady;
    [SerializeField] private UnityEvent OnShoot;
    [SerializeField] private UnityEvent OnBoomed;
    [SerializeField] private float EndToReadyTime = 1f;
    [SerializeField] private float RayLength = 3f;
    private RaycastHit2D[] hits;
    private WaitForSeconds endToReadyTimeWFS;
    public bool CanShoot => State == EState.Ready;
    private void Awake()
    {
        TryGetComponent(out _rigid);
        endToReadyTimeWFS = new WaitForSeconds(EndToReadyTime);
    }

    public void Update()
    {
        switch (State)
        {
            case EState.Ready:
            case EState.End:
                break;
            case EState.Shoot:
                ShootLogicUpdate();
                break;
            default:
                Debug.Assert(false, "Add case");
                return;
        }
    }

    public void FixedUpdate()
    {
        switch (State)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(BounceAbleWall) || State != EState.Shoot)
        {
            return;
        }
        
        Bounce();
    }

    private void Bounce()
    {
        transform.rotation = ConvertQuaternion(Dir);
        curBounceCount++;
        if (curBounceCount > MaxBounceCount)
        {
            StartCoroutine(Boom());
        }
    }

    public Quaternion ConvertQuaternion(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
        return Quaternion.Euler(new Vector3(0, 0, angle));
    }
    
    public void Shoot(Vector2 dir)
    {
        Debug.Assert(CanShoot, "Can not shoot");

        transform.rotation = ConvertQuaternion(dir);
        
        OnShoot.Invoke();
        State = EState.Shoot;
    }

    public void SetPosition(Transform other)
    {
        transform.position = other.position;
    }
    private IEnumerator Boom()
    {
        State = EState.End;
        _rigid.velocity = Vector2.zero;
        OnBoomed.Invoke();
        yield return endToReadyTimeWFS;
        Ready();
    }

    private void Ready()
    {
        State = EState.Ready;
        curBounceCount = 0;
        _rigid.velocity = Vector2.zero;
        OnReady.Invoke();
    }
    private void ShootLogicUpdate()
    {
        _rigid.velocity = transform.right * Speed;
    }
    
    private void ShootPhysicsUpdate()
    {
        Debug.DrawRay(transform.position, transform.right * RayLength, Color.green);
        hits = Physics2D.RaycastAll(transform.position, transform.right, RayLength);
        foreach (var hit in hits)
        {
            if (!hit.transform.CompareTag(BounceAbleWall))
            {
                continue;
            }   
            Vector2 normal = hit.normal;
            Debug.DrawRay(hit.point, normal * RayLength, Color.blue, 1f);

            Vector2 inDirection = transform.right;
            Debug.DrawRay(hit.point, inDirection * RayLength, Color.green, 1f);


            Dir = Vector2.Reflect(inDirection, normal);
            Debug.DrawRay(hit.point, Dir * RayLength, Color.red, 1f);

            break;
        }
    }
}