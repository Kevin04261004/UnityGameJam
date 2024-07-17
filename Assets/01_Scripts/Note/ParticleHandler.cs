using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    private List<ParticleSystem> particleSystems = new List<ParticleSystem>();
    [SerializeField] private Transform note;
    private void Awake()
    {
        foreach (Transform t in transform)
        {
            if (!t.TryGetComponent(out ParticleSystem ps))
            {
                continue;
            }
            if (particleSystems.Contains(ps))
            {
                continue;
            }
            particleSystems.Add(ps);
        }
    }

    private void Update()
    {
        SetAllParticlesShapePosition(note);
    }

    public void StopParticles()
    {
        foreach (var ps in particleSystems)
        {
            ps.Stop();
        }
    }
    private void SetAllParticlesShapePosition(Transform tr)
    {
        foreach (var ps in particleSystems)
        {
            ParticleSystem.ShapeModule shapeModule = ps.shape;
            shapeModule.position = tr.position;
            shapeModule.rotation = tr.eulerAngles;
        }
    }
}
