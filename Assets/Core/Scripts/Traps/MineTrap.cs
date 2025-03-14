using System.Linq;
using RootMotion.Dynamics;
using UnityEngine;

public class MineTrap : TrapBase
{
    public string[] muscleNames;

    public float explosionRadius = 5f;
    public float explosionForce = 20f;

    protected async override void ActivateTrap(PuppetMaster puppetMaster, Vector3 hitPoint)
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