using Entities.Weapons;
using UnityEngine;

namespace Game.PlayerMechanics.Battle
{
    public class BattleController : MonoBehaviour
    {
        public static BattleController Instance { get; private set; }
        private BattleManager _battleManager;
        
        private void Start()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            _battleManager = GetComponent<BattleManager>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && Player.Instance.LeftHand)
            {
                _battleManager.SetBlockShieldStatus(true);    
            }

            if (Input.GetKeyUp(KeyCode.Mouse1) && Player.Instance.LeftHand)
            {
                _battleManager.SetBlockShieldStatus(false);  
            }

            if (!Input.GetKeyDown(KeyCode.Mouse0)) return;
            
            if (Player.Instance.RightHand)
            {
                _battleManager.AttackWithWeapon(Player.Instance.RightHand.WeaponDataSO);
                return;
            }
            _battleManager.AttackWithHands();
        }

        private void GetInput()
        {
            
        }
    }
}