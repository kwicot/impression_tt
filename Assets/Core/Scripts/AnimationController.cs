using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Scripts
{
    [RequireComponent(typeof(MovementController))]
    public class AnimationController : MonoBehaviour
    {
        public MovementController m_movementController;
        public Animator m_animator;
        
        public string moveBlendName = "Move_Blend";
        public string jumpStateName = "Jump";
        public string failingStateName = "Fall";

        public string speedParameterName = "speed";


        private void Awake()
        {
            m_movementController = GetComponent<MovementController>();

            m_movementController.OnJump += OnJump;
            m_movementController.OnFailing += OnFailing;
            m_movementController.OnLanding += OnLanding;
        }

        private void Update()
        {
            m_animator.SetFloat(speedParameterName, m_movementController.Velocity.magnitude);
        }

        private void OnLanding()
        {
            m_animator.CrossFade(moveBlendName, 0.1f);
        }

        private void OnFailing()
        {
            m_animator.CrossFade(failingStateName, 0.1f);
        }

        private void OnJump()
        {
            m_animator.CrossFade(jumpStateName, 0.1f);
        }

    }
}