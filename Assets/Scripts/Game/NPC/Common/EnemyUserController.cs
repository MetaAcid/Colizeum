using System;
using Game.NPC;
using UnityEngine;

namespace NPC.Common
{
    public class EnemyUserController : MonoBehaviour
    {
        [SerializeField] private Enemy enemy;
        [SerializeField] private float damage = 10f;
        [SerializeField] private float health = 10f;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                enemy.HealthProperty.Value -= health;
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                enemy.HealthProperty.Value += health;
            }
        }
    }
}