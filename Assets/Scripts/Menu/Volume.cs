using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class Volume : MonoBehaviour
    {
        [SerializeField] private static bool _start;
        [SerializeField] private static float _soundVolume;
        [SerializeField] private Slider volumeSlider;
        
        private void Start()
        {
            if (_start == true)
            {
                AudioListener.volume = _soundVolume;
                volumeSlider.value = _soundVolume;
            }
            
        }

        public void ChangeVolume()
        {
            AudioListener.volume = volumeSlider.value;
            _soundVolume = volumeSlider.value;
            _start = true;
        }
    
        private void Update()
        {
        
        }
    }
}
