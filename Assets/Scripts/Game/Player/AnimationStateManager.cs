using System;
using Common;
using UnityEngine;

namespace Game.User
{
    public class AnimationStateManager : MonoBehaviour
    {
        public Animator animator;
        private int _state;
        private MovementManager _movementManager;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            ManageAnimations();
            animator.SetInteger(AnimationsConfig.AnimationMovement, _state);
        }

        private void ManageAnimations()
        {
            //if(_movementManager.Walk())
        }
    }
}