using System;
using UnityEngine;

namespace Game.Flocking
{
    public struct BirdDistance
    {
        public Bird bird;
        public float distance;

        public BirdDistance(float distance, Bird bird)
        {
            this.bird = bird;
            this.distance = distance;
        }
    }
    
    // К центру
    // К друг другу
    // Замедляться (если близко)
    public class Bird : MonoBehaviour
    {
        [Header("Flock center")]
        [SerializeField] private Transform center;
        
        [Header("Movement properties")]
        [SerializeField] private float maxSpeed = 10;
        [SerializeField] private float minSpeed = 2;
        [SerializeField] private float minDistanceToBird = 1;
        [SerializeField] private Transform forwardPoint;
        [SerializeField] private Transform backwardPoint;
        
        [Header("Searching properties")]
        [SerializeField] private float searchRadius;
        
        private float _currentSpeedFollow;
        private float _currentSpeedToCenter = 0.5f;
        private Bird _nearestBird;

        private void Awake()
        {
            _currentSpeedFollow = maxSpeed / 2;
        }

        private void Update()
        {
            _nearestBird = GetNearestBird();
            transform.LookAt(_nearestBird.transform);
            
            _currentSpeedFollow = maxSpeed;
            
            if (IsBirdClose(_nearestBird))
            {
                _currentSpeedFollow = minSpeed;
                if (_nearestBird.GetDistanceToCenter() < GetDistanceToCenter())
                {
                    Move(backwardPoint.position, _currentSpeedFollow);
                    return;
                }
                Move(center.position, _currentSpeedToCenter);
                return;
            }
            
            Move(center.position, _currentSpeedToCenter);
            Move(_nearestBird.transform.position, _currentSpeedFollow);
        }

        private bool IsBirdClose(Bird bird)
        {
            Debug.Log(Vector3.Distance(bird.transform.position, transform.position));
            return Vector3.Distance(bird.transform.position, transform.position) < minDistanceToBird;
        }

        public float GetDistanceToCenter()
        {
            return Vector3.Distance(center.position, transform.position);
        }

        private void Move(Vector3 direction, float speed)
        {
            Vector3 newPosition = Vector3.MoveTowards(
                transform.position,
                direction,
                speed * Time.deltaTime);

            transform.position = newPosition;
        }

        private Bird GetNearestBird()
        {
            BirdDistance result = new BirdDistance(float.MaxValue, null);
            Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);

            foreach (var col in colliders)
            {
                if (!col.TryGetComponent<Bird>(out var bird) || col.gameObject == gameObject) continue;

                if (result.distance > Vector3.Distance(bird.transform.position, transform.position))
                {
                    result.distance = Vector3.Distance(bird.transform.position, transform.position);
                    result.bird = bird;
                }
            }

            return result.bird;
        }

        private Bird GetLongestBird()
        {
            BirdDistance result = new BirdDistance(float.MinValue, null);
            Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);

            foreach (var col in colliders)
            {
                if (!col.TryGetComponent<Bird>(out var bird) || col.gameObject == gameObject) continue;

                if (result.distance < Vector3.Distance(bird.transform.position, transform.position))
                {
                    result.distance = Vector3.Distance(bird.transform.position, transform.position);
                    result.bird = bird;
                }
            }

            return result.bird;
        }
    }
}

