using UnityEngine;

public class SetPositionComponent : MonoBehaviour
{
    [SerializeField] private Vector3 pos;
    [SerializeField] private bool _localPos = true;
    
    public void SetPos()
    {
        if (_localPos)
        {
            transform.localPosition = pos;
        }
        else
        {
            transform.position = pos;
        }
    }
}
