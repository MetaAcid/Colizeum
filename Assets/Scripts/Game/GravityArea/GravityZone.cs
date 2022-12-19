using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.GravityArea
{
    public class GravityZone : MonoBehaviour
    {
        [SerializeField] private UnityEvent onIncreaseGravity;
        [SerializeField] private UnityEvent onDecreaseGravity;
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IGravityChangeable>() == null) return;
            
            other.GetComponent<IGravityChangeable>().IncreaseGravity();
            onIncreaseGravity.Invoke();
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<IGravityChangeable>() == null) return;
            
            other.GetComponent<IGravityChangeable>().ResetGravity();
            onDecreaseGravity.Invoke();
        }
    }
}
