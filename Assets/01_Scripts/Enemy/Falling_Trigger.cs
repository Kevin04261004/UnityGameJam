using UnityEngine;

public class Falling_Trigger : MonoBehaviour
{
    [SerializeField] private RoofGenerator _roofGenerator;
    [SerializeField] private string targetTag;
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag != targetTag)
        {
            return;
        }

        if (_roofGenerator != null)
        {
            _roofGenerator.Fallobjects();
        }
        
    }
}
