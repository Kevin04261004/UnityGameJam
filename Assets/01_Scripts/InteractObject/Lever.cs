using System;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour, IInteractableObject
{
    private bool IsOn => Math.Abs(lever.eulerAngles.z - 45f) >= 1f;
    public Transform lever;
    public UnityEvent OnTriggerOn;
    public UnityEvent OnTriggerOff;
    public void Interact()
    {
        if (IsOn) // true -> false
        {
            lever.eulerAngles = new Vector3(0, 0, 45f);
            OnTriggerOff.Invoke();
        }
        else // false -> true
        {
            lever.eulerAngles = new Vector3(0, 0, -45f);
            OnTriggerOn.Invoke();
        }
    }
}
