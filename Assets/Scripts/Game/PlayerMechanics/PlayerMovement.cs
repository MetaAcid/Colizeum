using System;
using Common;
using Game.GravityArea;
using UnityEngine;

namespace Game.PlayerMechanics
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerMechanics.Player player;
        [SerializeField] private bool ground;
        [SerializeField] private Rigidbody playerRigidbody;
        
        public Action onPlayerRun = () => {};
        public Action onPlayerJump = (() => {});
        
        public int state;

        public void Run(float speed)
        {
            onPlayerRun?.Invoke();
            
            state = 2;
            transform.localPosition += transform.forward * (speed * Time.deltaTime);
        }

        public void Walk(float speed)
        {
            state = 1;
            transform.localPosition += transform.forward * (speed * Time.deltaTime);
        }

        public void WalkSide(float speed)
        {
            if (Input.GetKey(KeyCode.A))
            {
                state = 3;
                player.transform.localPosition += -transform.right * (speed * Time.deltaTime);
            }
            
            if (Input.GetKey(KeyCode.D))
            {
                state = 4;
                player.transform.localPosition += transform.right * (speed * Time.deltaTime);
            }
        }

        public void WalkingBack(float speed)
        {
            state = 6;
            player.transform.localPosition += -transform.forward * (speed * Time.deltaTime);
        }

        public void Jumping(float jumpPower)
        {
            if (!ground) return;
            
            onPlayerJump?.Invoke();
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            {
                state = 7;
                playerRigidbody.AddForce(transform.up * (jumpPower * 1.1f));
            }
            else
            {
                state = 5;
                playerRigidbody.AddForce(transform.up * jumpPower);
            }
        }

        public void Idle()
        {
            state = 0;
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