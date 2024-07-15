using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementData", menuName = "Scriptable Object/PlayerMovementData SO", order = 0)]
public class PlayerMovementData : ScriptableObject
{
    [Header("Move")]
    [field: SerializeField] public float SprintSpeed { get; set; }
    [field: SerializeField] public float WalkSpeed { get; set; }
    
    [Header("Jump")]
    [field: SerializeField] public float JumpStrength { get; set; }
}
