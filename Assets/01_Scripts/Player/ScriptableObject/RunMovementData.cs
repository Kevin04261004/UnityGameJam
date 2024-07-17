using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementData", menuName = "Scriptable Object/RunMovementData SO", order = 0)]
public class RunMovementData : ScriptableObject
{
    [Header("Move")]
    [field: SerializeField] public float SprintSpeed { get; set; }
    [field: SerializeField] public float WalkSpeed { get; set; }
    
    [Header("Jump")]
    [field: SerializeField] public float JumpStrength { get; set; }
    
    [Header("Action")]
    [field:SerializeField] public float DamagePower { get; set; }
    
    
}
