using Entities.Weapons;
using UnityEngine;

namespace Game.PlayerMechanics.Battle
{
    public class BattleController : MonoBehaviour
    {
        public static BattleController Instance { get; private set; }
        private WeaponDataSO _weapon;
        private BattleManager _battleManager;
        
        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            _weapon = Player.Instance.GetComponent<WeaponDataSO>();
            _battleManager = GetComponent<BattleManager>();
        }

        private void Update()
        {
            GetInput();
        }

        private void GetInput()
        {
            _battleManager.AttackWithHands();
            _battleManager.BlockWithShield(_weapon);
        }
    }
}