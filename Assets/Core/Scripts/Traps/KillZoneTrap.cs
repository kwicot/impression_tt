using System.Linq;
using RootMotion.Dynamics;
using UnityEngine;

namespace Core.Scripts.Traps
{
    
    public class KillZoneTrap : TrapBase
    {
        public string[] muscleNames;

        public float explosionRadius = 10f;
        public float explosionForce = 30f;

        protected override void ActivateTrap(PuppetMaster puppetMaster, Vector3 hitPoint)
        {
            EnableRagdoll(puppetMaster);
            var muscles = puppetMaster.muscles.ToArray();
            for (int i = muscles.Length - 1; i >= 0; i--)
            {
                if (muscleNames.Contains(muscles[i].name))
                {
                    puppetMaster.RemoveMuscleRecursive(muscles[i].joint, true);
                    muscles[i].rigidbody.AddExplosionForce(explosionForce, hitPoint, explosionRadius, 1f, ForceMode.Impulse);
                }
            }
        }
    }
}