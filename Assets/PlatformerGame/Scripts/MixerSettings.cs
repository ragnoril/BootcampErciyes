using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace PlatformerGame
{

    public class MixerSettings : MonoBehaviour
    {
        public AudioMixer Mixer;

        public float MusicVolume;
        public float SfxVolume;

        public bool IsMusicMute;
        public bool IsSfxMute;

        private void Awake()
        {
            MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
            SfxVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);
            
            int mute = PlayerPrefs.GetInt("MuteMusic", 0);
            if (mute == 0)
                IsMusicMute = false;
            else
                IsMusicMute = true;

            mute = PlayerPrefs.GetInt("MuteSound", 0);
            if (mute == 0)
                IsSfxMute = false;
            else
                IsSfxMute = true;
        }

        private void Start()
        {
            SetMusicVolume(MusicVolume);
            SetSfxVolume(SfxVolume);
            MuteMusic(IsMusicMute);
            MuteSfx(IsSfxMute);
        }

        public void MuteMusic(bool isMute)
        {
            if (isMute)
            {
                float oldValue = MusicVolume;
                SetMusicVolume(0f);
                MusicVolume = oldValue;
                PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
                IsMusicMute = true;
                PlayerPrefs.SetInt("MuteMusic", 1);
            }
            else
            {
                SetMusicVolume(MusicVolume);
                PlayerPrefs.SetInt("MuteMusic", 0);
                IsMusicMute = false;
            }
        }

        public void MuteSfx(bool isMute)
        {
            if (isMute)
            {
                float oldValue = SfxVolume;
                SetSfxVolume(0f);
                SfxVolume = oldValue;
                PlayerPrefs.SetFloat("SoundVolume", SfxVolume);
                IsSfxMute = true;
                PlayerPrefs.SetInt("MuteSound", 1);
            }
            else
            {
                SetSfxVolume(SfxVolume);
                PlayerPrefs.SetInt("MuteSound", 0);
                IsSfxMute = false;
            }
        }

        public void SetMusicVolume(float value)
        {
            MusicVolume = value;
            PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
            Mixer.SetFloat("musicVol", ((value - 1f) * 80f));
        }

        public void SetSfxVolume(float value)
        {
            SfxVolume = value;
            PlayerPrefs.SetFloat("SoundVolume", SfxVolume);
            Mixer.SetFloat("sfxVol", ((value - 1f) * 80f));
        }

        public float GetMusicVolume()
        {
            float volume = 0f;
            Mixer.GetFloat("musicVol", out volume);
            volume = (volume / 80f) + 1f;

            return volume;
        }

        public float GetSfxVolume()
        {
            float volume = 0f;
            Mixer.GetFloat("sfxVol", out volume);
            volume = (volume / 80f) + 1f;

            return volume;
        }

    }

}