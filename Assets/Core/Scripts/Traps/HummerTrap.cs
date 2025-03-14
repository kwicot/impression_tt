using RootMotion.Dynamics;
using UnityEngine;

public class HammerTrap : TrapBase
{
    public Vector3 hitDirection = Vector3.forward;

    protected override void ActivateTrap(PuppetMaster puppetMaster, Vector3 hitPoint)
    {
        EnableRagdoll(puppetMaster);
        ApplyForceToRagdoll(puppetMaster, hitDirection.normalized * forceMultiplier, hitPoint);
    }
}