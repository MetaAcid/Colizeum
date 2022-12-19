using System;
using UnityEngine;

namespace Game.Flocking
{
    public class CenterMovement : MonoBehaviour
    {
        [SerializeField] private float speedRotation = 5;

        private void Update()
        {
            transform.Rotate(Vector3.up * speedRotation * Time.deltaTime);
        }
    }
}