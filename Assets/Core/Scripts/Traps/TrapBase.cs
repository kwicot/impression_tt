using System;
using Core.Scripts;
using RootMotion.Dynamics;
using UnityEngine;

public abstract class TrapBase : MonoBehaviour
{
    public float forceMultiplier = 10f;

    protected virtual void OnCollisionEnter(Collision other)
    {
        var puppetMaster = other.collider.GetComponentInChildren<PuppetMaster>();
        var movement = other.collider.GetComponentInParent<MovementController>();
        
        if(puppetMaster == null || movement == null)
            return;
        
        movement.canMove = false;
        
        if (puppetMaster != null)
        {
            ActivateTrap(puppetMaster, other.collider.ClosestPoint(transform.position));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var puppetMaster = other.GetComponentInChildren<PuppetMaster>();
        var movement = other.GetComponentInParent<MovementController>();
        
        if(puppetMaster == null || movement == null)
            return;
        
        movement.canMove = false;
        
        if (puppetMaster != null)
        {
            ActivateTrap(puppetMaster, other.ClosestPoint(transform.position));
        }
    }

    protected abstract void ActivateTrap(PuppetMaster puppetMaster, Vector3 hitPoint);

    protected void EnableRagdoll(PuppetMaster puppetMaster)
    {
        puppetMaster.state = PuppetMaster.State.Dead;
        puppetMaster.pinWeight = 0;
    }

    protected void ApplyForceToRagdoll(PuppetMaster puppetMaster, Vector3 force, Vector3 hitPoint)
    {
        foreach (var muscle in puppetMaster.muscles)
        {
            muscle.rigidbody.AddForceAtPosition(force, hitPoint, ForceMode.Impulse);
        }
    }
}