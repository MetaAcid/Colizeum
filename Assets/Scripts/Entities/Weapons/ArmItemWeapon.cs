using UnityEngine;

namespace Entities.Weapons
{
    public class ArmItemWeapon : MonoBehaviour
    {
        [SerializeField] private WeaponDataSO weaponDataSO;

        public WeaponDataSO WeaponDataSO => weaponDataSO;

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