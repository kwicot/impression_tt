using PLAYERTWO.PlatformerProject;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using PlayerInputManager = PLAYERTWO.PlatformerProject.PlayerInputManager;

namespace Core.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerInputManager))]
    public class MovementController : MonoBehaviour
    {
        public enum PlayerState { Idle, Moving, Jumping, Falling }
        
        public Transform m_cameraTransform;
        
        [Header("Movement Settings")]
        public float moveSpeed = 5f;
        public float rotationSpeed = 10f;
        public float jumpForce = 7f;
        
        [Header("Ground Check")]
        public Transform groundCheck; 
        public LayerMask groundMask;
        public float groundCheckRadius = 0.2f;
        
        private PlayerInputManager m_inputManager;
        private Rigidbody m_rigidbody;
        
        public PlayerState currentState = PlayerState.Idle;
        private bool isGrounded;
        private Vector3 previousPosition;

        
        public UnityAction OnJump;
        public UnityAction OnFailing;
        public UnityAction OnLanding;
        
        public bool canMove = true;

        public Vector3 Velocity { get; private set; }

        public enum State
        {
            Idle,
            Moving,
            Jumping,
            Failing
        }


        void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_inputManager = GetComponent<PlayerInputManager>();
            
            m_rigidbody.freezeRotation = true;
        }
        
        void FixedUpdate()
        {
            GroundCheck();
            Move();
            
            CalculateVelocity();
            HandleState();
            Jump();
        }

        void Move()
        {
            if(!canMove)
                return;
            
            isGrounded = Physics.CheckSphere(transform.position, groundCheckRadius, groundMask);

            Vector3 direction = m_inputManager.GetMovementDirection();

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + m_cameraTransform.eulerAngles.y;
                float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, Time.deltaTime * rotationSpeed);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                var targetVelocity = moveDir * Time.deltaTime * moveSpeed;
                m_rigidbody.MovePosition(m_rigidbody.position + targetVelocity);

                if (currentState != PlayerState.Jumping && currentState != PlayerState.Falling)
                {
                    ChangeState(PlayerState.Moving);
                }
            }
            else if (isGrounded && currentState != PlayerState.Jumping && currentState != PlayerState.Falling)
            {
                ChangeState(PlayerState.Idle);
            }
        }
        
        void Jump()
        {
            if(!canMove || !isGrounded)
                return;
            
            if (m_inputManager.GetJumpDown())
            {
                m_rigidbody.linearVelocity =
                    new Vector3(m_rigidbody.linearVelocity.x, jumpForce, m_rigidbody.linearVelocity.z);
                ChangeState(PlayerState.Jumping);
                OnJump?.Invoke();
            }
        }

        void GroundCheck()
        {
            bool wasGrounded = isGrounded;
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);

            if (!wasGrounded && isGrounded)
            {
                ChangeState(PlayerState.Idle);
                OnLanding?.Invoke();
            }
            else if (!isGrounded && m_rigidbody.linearVelocity.y < -0.1f)
            {
                ChangeState(PlayerState.Falling);
                OnFailing?.Invoke();
            }
        }
        void CalculateVelocity()
        {
            Velocity = (transform.position - previousPosition) / Time.fixedDeltaTime / moveSpeed;
            previousPosition = transform.position;
        }

        void HandleState()
        {
            // Логика состояний, если понадобится
        }

        void ChangeState(PlayerState newState)
        {
            if (currentState != newState)
            {
                currentState = newState;
            }
        }
    }
}