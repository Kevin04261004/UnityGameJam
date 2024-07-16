using UnityEngine;
using UnityEngine.Events;

public class CallEventWhenStart : MonoBehaviour
{
    [SerializeField] private UnityEvent OnStart;
    void Start()
    {
        OnStart.Invoke();
    }

}
