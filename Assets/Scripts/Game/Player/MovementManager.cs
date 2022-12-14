using UnityEngine;
using System;
using Common;

namespace Game.User
{
    public class MovementManager : MonoBehaviour
    {
        [SerializeField] private Player.Player player;
        [SerializeField] private bool ground;
        [SerializeField] private Rigidbody playerRigidbody;
        
        public Action onPlayerRun;
        public Action onPlayerJump;
        
        private int _state;
        
        
        public void Run(float speed)
        {
            onPlayerRun?.Invoke();
            
            _state = 2;
            transform.localPosition += transform.forward * (speed * Time.deltaTime);
        }

        public void Walk(float speed)
        {
            _state = 1;
            transform.localPosition += transform.forward * (speed * Time.deltaTime);
        }

        public void WalkSide(float speed)
        {
            if (Input.GetKey(KeyCode.A))
            {
                _state = 3;
                player.transform.localPosition += -transform.right * (speed * Time.deltaTime);
            }
            
            if (Input.GetKey(KeyCode.D))
            {
                _state = 4;
                player.transform.localPosition += transform.right * (speed * Time.deltaTime);
            }
        }

        public void WalkingBack(float speed)
        {
            _state = 6;
            player.transform.localPosition += -transform.forward * (speed * Time.deltaTime);
        }

        public void Jumping(float jumpPower)
        {
            if (!ground) return;
            
            onPlayerJump?.Invoke();
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            {
                _state = 7;
                playerRigidbody.AddForce(transform.up * (jumpPower * 1.1f));
            }
            else
            {
                _state = 5;
                playerRigidbody.AddForce(transform.up * jumpPower);
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
    }
}