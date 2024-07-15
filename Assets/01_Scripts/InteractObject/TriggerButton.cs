using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerButton : MonoBehaviour
{
    [SerializeField] private List<Collider2D> _colliders;
    [SerializeField] private UnityEvent OnTriggerOn;
    [SerializeField] private UnityEvent OnTriggerOff;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("PushableObject") && !other.CompareTag("Player"))
        {
            return;
        }
        _colliders.Add(other);
        OnTriggerOn.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_colliders.Contains(other))
        {
            _colliders.Remove(other);
        }

        if (_colliders.Count <= 0)
        {
            OnTriggerOff.Invoke();
        }
    }
}
