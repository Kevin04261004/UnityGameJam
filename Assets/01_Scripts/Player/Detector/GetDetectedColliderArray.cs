using System;
using System.Collections.Generic;
using UnityEngine;

public class GetDetectedColliderArray : MonoBehaviour
{
    [field: SerializeField] public List<Collider2D> DetectedColliders { get; set; }
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private bool _dontAddWhenOtherColliderIsTrigger;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger && _dontAddWhenOtherColliderIsTrigger)
        {
            return;
        }
        if (((1 << other.gameObject.layer) & _layerMask) != 0)
        {
            DetectedColliders.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!DetectedColliders.Contains(other))
        {
            return;   
        }
        DetectedColliders.Remove(other);
    }
}
