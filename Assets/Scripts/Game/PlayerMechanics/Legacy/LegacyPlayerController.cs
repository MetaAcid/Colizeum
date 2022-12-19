using System;
using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Player.Legacy
{
    public class LegacyPlayerController : MonoBehaviour
    {
        public static LegacyPlayerController Instance { get; private set; }
    
        public Animator anim;
        public Image healthBar;
        public int playerHealth;
        private int _state;
        public float speed = 7f;
        private float maxSpeed = 10f;
        public float jumpPower = 200f;
        public bool ground;
        public Rigidbody rb;

        public Action OnPlayerJump;
        public Action OnPlayerRun;

        //public delegate void PlayerJump();
        //public event PlayerJump OnPlayerJump1;

        //public UnityEvent OnPlayerRun1;

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            HealthCalc();
            GetInput();

            anim.SetInteger("state", _state);
        }

        private void GetInput()
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    OnPlayerRun?.Invoke();
                    _state = 2;
                    transform.localPosition += transform.forward * maxSpeed * Time.deltaTime;
                }
                else
                {
                    _state = 1;
                    transform.localPosition += transform.forward * speed * Time.deltaTime;
                }
            }
            else
            {
                _state = 0;
            }


            if (Input.GetKey(KeyCode.S))
            {
                _state = 6;
                transform.localPosition += -transform.forward * speed * Time.deltaTime;
            }


            if (Input.GetKey(KeyCode.A))
            {
                _state = 3;
                transform.localPosition += -transform.right * speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.D))
            {
                _state = 4;
                transform.localPosition += transform.right * speed * Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!ground) return;
            
                OnPlayerJump?.Invoke();
                if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
                {
                    _state = 7;
                    rb.AddForce(transform.up * jumpPower * 1.1f);
                }
                else
                {
                    _state = 5;
                    rb.AddForce(transform.up * jumpPower);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(TagConfig.GroundLayer))
            {
                ground = true;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag(TagConfig.GroundLayer))
            {
                ground = false;
            }
        }

        private void HealthCalc()
        {
            float currentHealth = playerHealth / 100;

            if (currentHealth < 1.0)
            {
                healthBar.fillAmount = currentHealth;
            }
        }
    }
}
