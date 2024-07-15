using System.Collections.Generic;
using UnityEngine;

public class Enemy_Patrol : Abstract_Enemy
{
    [SerializeField] private List<Transform> patrolPoints;
    
    protected override void Die()
    {
        throw new System.NotImplementedException();
    }
}
