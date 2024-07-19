using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    [SerializeField] private UnityEvent eventAction;
    public void Active()
    {
        eventAction.Invoke();
    }
}
