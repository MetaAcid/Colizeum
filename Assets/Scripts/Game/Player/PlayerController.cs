using Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.User
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        [Header("Base properties")]
        [SerializeField] private Player.Player player;

        [Header("Movement properties")]
        [SerializeField] private float speed = 7f;
        [SerializeField] private float jumpPower = 200f;

        private float _maxSpeed = 10f;
        private MovementManager _movementManager;
        
        
        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            _movementManager = GetComponent<MovementManager>();
        }

        private void Update()
        {
            GetInput();
        }

        private void GetInput()
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _movementManager.Run(_maxSpeed);
                }
                else
                {
                    _movementManager.Walk(speed);
                }
            }
            
            if (Input.GetKey(KeyCode.S))
            {
                _movementManager.WalkingBack(speed);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _movementManager.Jumping(jumpPower);
            }
            
            _movementManager.WalkSide(speed);
        }
        
        
    }
}