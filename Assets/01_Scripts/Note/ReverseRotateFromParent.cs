using System;
using UnityEngine;

public class ReverseRotateFromParent : MonoBehaviour
{
    private void Update()
    {
        var parent = transform.parent;
        var eulerAngles = parent.rotation.eulerAngles;
        eulerAngles.z *= -1f;
        transform.localRotation = Quaternion.Euler(eulerAngles);
    }
}
