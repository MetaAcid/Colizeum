using UnityEngine;
using UnityEngine.Serialization;

namespace Entities.Shields
{
    public class ArmItemShield : MonoBehaviour
    {
        [SerializeField] private ShieldDataSO shieldDataSO;

        public ShieldDataSO ShieldDataSO => shieldDataSO;

        public void Wear()
        {
            gameObject.SetActive(true);
        }

        public void Drop()
        {
            Destroy(gameObject);
        }
    }
}