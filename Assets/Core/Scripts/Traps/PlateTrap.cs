using RootMotion.Dynamics;
using UnityEngine;

namespace Core.Scripts.Traps
{
    public class PlateTrap : TrapBase
    {
        public Vector3 forceVector;
        protected override void ActivateTrap(PuppetMaster puppetMaster, Vector3 hitPoint)
        {
            EnableRagdoll(puppetMaster);
            Vector3 forceDirection = (hitPoint - transform.position).normalized + forceVector * 0.5f;
            ApplyForceToRagdoll(puppetMaster, forceDirection * forceMultiplier, hitPoint);
        }
    }
}