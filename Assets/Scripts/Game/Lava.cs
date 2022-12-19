using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Lava : MonoBehaviour
    {
        [SerializeField] private Transform targetPoint;
        [SerializeField] private float speedMovement = 1;
        [SerializeField] private float damage = 1;
        [SerializeField] private float damageRate = 0.3f;

        [SerializeField] private UnityEvent onPlayerEnter;
        [SerializeField] private UnityEvent onPlayerExit;

        private bool _isLaunched = false;
        private IEnumerator _attackCoroutine;

        private void Start()
        {
            _attackCoroutine = AttackDelay();
        }

        private void Update()
        {
            if (!_isLaunched) return;
            
            Move();
        }

        private void OnTriggerEnter(Collider col)
        {
            if (!col.TryGetComponent<PlayerMechanics.Player>(out var player)) return;
            
            StartCoroutine(_attackCoroutine);
            onPlayerEnter.Invoke();
        }

        private void OnTriggerExit(Collider col)
        {
            if (!col.TryGetComponent<PlayerMechanics.Player>(out var player)) return;
            
            StopCoroutine(_attackCoroutine);
            onPlayerExit.Invoke();
        }

        private void Move()
        {
            Vector3 newPosition = Vector3.MoveTowards(
                transform.position,
                targetPoint.position,
                speedMovement * Time.deltaTime);

            transform.position = newPosition;
        }

        private IEnumerator AttackDelay()
        {
            while (true)
            {
                PlayerMechanics.Player.Instance.HealthProperty.Value -= damage;
                yield return new WaitForSeconds(damageRate);
            }
        }
        
        public void Launch()
        {
            _isLaunched = true;
        }
    }
}
