using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementData", menuName = "Scriptable Object/LampMovementData SO", order = 0)]
public class LampMovementData : ScriptableObject
{
    [Header("Move")]
    [field: SerializeField] public float Speed { get; set; }
    
    [Header("Jump")]
    [field: SerializeField] public float JumpStrength { get; set; }
}