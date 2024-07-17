using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Falling_Object : MonoBehaviour
{
    [SerializeField] private float fallSpeed;
    private float currentSpeed = 0;

    public float FallSpeed
    {
        get => fallSpeed;
        set
        {
            fallSpeed = value;
            currentSpeed = fallSpeed * Time.fixedDeltaTime;
        }
    }
    
   
    private bool _isFall;

    private void Awake()
    {
        currentSpeed = fallSpeed * Time.fixedDeltaTime;
    }

    public bool IsFall
    {
        get => _isFall;
        set => _isFall = value;
    }
    

    private void FixedUpdate()
    {
        if (_isFall)
        {
            transform.Translate(Vector3.down*currentSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
        {
            _isFall = false;
            Destroy(this);
        }

        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            Vector2 dirVec =  other.transform.position - this.transform.position;
            dirVec.Normalize();

            float dot = Vector2.Dot(dirVec, Vector2.down);
            if (dot >= 0)
            {
                // TODO -> Add GameOver Action      
                Debug.Log("GameOver!");
            }
            
        }

    }
    
}
