using Common;
using Game.GravityArea;
using Game.PlayerMechanics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.User
{
    public class PlayerController : MonoBehaviour, IGravityChangeable
    {
        public static PlayerController Instance { get; private set; }

        [Header("Base properties")]
        [SerializeField] private PlayerMechanics.Player player;
        [SerializeField] private Animator animator;

        [Header("Movement properties")]
        [SerializeField] private float speed = 7f;
        [SerializeField] private float jumpPower = 200f;
        [SerializeField] private float gravityScaleFactor = 4f;

        private float _maxSpeed = 10f;
        private PlayerMovement _playerMovement;
        
        
        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            _playerMovement = GetComponent<PlayerMovement>();
        }
        
        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            GetInput();
            animator.SetInteger(AnimationsConfig.AnimationMovement, _playerMovement.state);
            _playerMovement.Idle();
        }

        private void GetInput()
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.LeftShift) && PlayerMechanics.Player.Instance.StaminaProperty.Value > 0)
                {
                    _playerMovement.Run(_maxSpeed);
                }
                else
                {
                    _playerMovement.Walk(speed);
                }
            }
            
            if (Input.GetKey(KeyCode.S))
            {
                _playerMovement.WalkingBack(speed);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _playerMovement.Jumping(jumpPower);
            }
            
            _playerMovement.WalkSide(speed);
        }


        public void IncreaseGravity()
        {
            jumpPower /= gravityScaleFactor;
            speed /= gravityScaleFactor;
            _maxSpeed /= gravityScaleFactor;
        }

        public void ResetGravity()
        {
            jumpPower *=  gravityScaleFactor;
            speed *=  gravityScaleFactor;
            _maxSpeed *=  gravityScaleFactor;
        }
    }
}