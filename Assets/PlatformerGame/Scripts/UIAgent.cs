using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlatformerGame
{
    public class UIAgent : MonoBehaviour
    {
        public MixerSettings Settings;

        public Slider MusicSlider;
        public Slider SoundSlider;

        public Toggle MusicToggle;
        public Toggle SoundToggle;

        private void Start()
        {
            /*
            MusicSlider.value = AudioManager.Instance.MusicVolume;
            SoundSlider.value = AudioManager.Instance.SoundVolume;

            MusicToggle.isOn = !AudioManager.Instance.IsMusicMute;
            SoundToggle.isOn = !AudioManager.Instance.IsSoundMute;
            */
            MusicSlider.value = Settings.MusicVolume; //Settings.GetMusicVolume();
            SoundSlider.value = Settings.SfxVolume; //Settings.GetSfxVolume();

            MusicToggle.isOn = !Settings.IsMusicMute;
            SoundToggle.isOn = !Settings.IsSfxMute;
        }

        public void ChangeMusicVolume(float value)
        {
            //AudioManager.Instance.SetMusicVolume(value);
            Settings.SetMusicVolume(value);
        }

        public void ChangeSfxVolume(float value)
        {
            //AudioManager.Instance.SetSoundVolume(value);
            Settings.SetSfxVolume(value);
        }

        public void SetMusicOnOff(bool value)
        {
            //AudioManager.Instance.MuteMusic(!value);
            Settings.MuteMusic(!value);
        }

        public void SetSfxOnOff(bool value)
        {
            //AudioManager.Instance.MuteSound(!value);
            Settings.MuteSfx(!value);
        }
    }

}
