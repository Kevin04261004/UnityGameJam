using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementData", menuName = "Scriptable Object/PlatformMovementData SO", order = 0)]
public class PlatformMovementData : ScriptableObject
{
    [Header("Move")]
    [field: SerializeField] public float SprintSpeed { get; set; }
    [field: SerializeField] public float WalkSpeed { get; set; }
    
    [Header("Jump")]
    [field: SerializeField] public float JumpStrength { get; set; }
}
