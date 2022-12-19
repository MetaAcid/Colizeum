using System;
using Game.NPC;
using UnityEngine;
using UnityEngine.Serialization;

namespace NPC.Common
{
    public class EnemyUserController : MonoBehaviour
    {
        [FormerlySerializedAs("enemy")] [SerializeField] private Satyr satyr;
        [SerializeField] private float damage = 10f;
        [SerializeField] private float health = 10f;

        private void Update()
        {
            if (!satyr) return;
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                satyr.HealthProperty.Value -= health;
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                satyr.HealthProperty.Value += health;
            }
        }
    }
}