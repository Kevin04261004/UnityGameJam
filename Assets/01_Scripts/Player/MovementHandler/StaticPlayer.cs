﻿using UnityEngine;

public class StaticPlayer : BasePlayer
{
    [SerializeField] private ;
    public override void HandleMovement()
    {
        base.HandleMovement();
        /* animator Update */
        _animator.SetBool(EAnimationKeys.Grounded.ToString(), _detector.Grounded);
        
        
    }

    public override void HandlePhysics()
    {
        base.HandlePhysics();
    }
}