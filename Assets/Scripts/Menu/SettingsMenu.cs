using UnityEngine;
using UnityEngine.Audio;

namespace Menu
{
    public class SettingsMenu : MonoBehaviour
    {

        public AudioMixer audioMixer;

        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("volume", volume);
        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }
    }
}
