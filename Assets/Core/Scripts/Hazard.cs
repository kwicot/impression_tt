using System;
using UnityEngine;

namespace Core.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class Hazard : MonoBehaviour
    {
        public enum HazardType {Hazard, Mine, Hammer, KillZone}

        public Vector3 ragdollVector;
        public float weightMultiplier;
        public float forceMultiplier;
        public HazardType hazardType;

        private Rigidbody m_rigibody;

        private void Awake()
        {
            m_rigibody = GetComponent<Rigidbody>();
        }

        public Vector3 GetDamageVector(Vector3 normal)
        {
            switch (hazardType)
            {
                case HazardType.Hazard:
                    return (ragdollVector + normal) * weightMultiplier * forceMultiplier;
                    break;
                case HazardType.Mine:
                case HazardType.KillZone:
                    gameObject.SetActive(false);
                    return Vector3.up * weightMultiplier * forceMultiplier;
                    break;
                case HazardType.Hammer:
                    return (normal * m_rigibody.angularVelocity.magnitude) * weightMultiplier * forceMultiplier;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}