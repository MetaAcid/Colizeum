using Game.User;
using UnityEngine;
using UnityEngine.UI;

namespace Game.PlayerMechanics
{
    public class PlayerPropertyUI : MonoBehaviour
    {
        [SerializeField] private Image fieldImage;

        private PlayerProperty _playerProperty;
        
        public PlayerProperty PlayerProperty
        {
            get => _playerProperty;
            set
            {
                _playerProperty = value;
                _playerProperty.OnValueChange += UpdateUI;
            }
        }

        private void UpdateUI()
        {
            fieldImage.fillAmount = _playerProperty.Value / _playerProperty.MaxValue;
        }
    }
}