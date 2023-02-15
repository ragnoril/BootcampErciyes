using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerGame
{

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }

            MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
            SoundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);

            int mute = PlayerPrefs.GetInt("MuteMusic", 0);
            if (mute == 0)
                IsMusicMute = false;
            else
                IsMusicMute = true;

            mute = PlayerPrefs.GetInt("MuteSound", 0);
            if (mute == 0)
                IsSoundMute = false;
            else
                IsSoundMute = true;
        }

        public AudioSource MusicPlayer;
        public AudioSource[] SoundChannels;

        public float MusicVolume;
        public float SoundVolume;

        public bool IsMusicMute;
        public bool IsSoundMute;

        private void Start()
        {
            SetMusicVolume(MusicVolume);
            SetSoundVolume(SoundVolume);
            MuteMusic(IsMusicMute);
            MuteSound(IsSoundMute);
        }

        public void PlayMusic(AudioClip musicClip)
        {
            MusicPlayer.clip = musicClip;
            MusicPlayer.Play();
        }

        public void PlaySound(AudioClip clip)
        {
            foreach (AudioSource channel in SoundChannels)
            {
                if (!channel.isPlaying)
                {
                    channel.PlayOneShot(clip);
                    break;
                }
            }
        }

        public void SetMusicVolume(float value)
        {
            MusicPlayer.volume = value;
            MusicVolume = value;
            PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        }

        public void SetSoundVolume(float value)
        {
            foreach (AudioSource channel in SoundChannels)
            {
                channel.volume = value;
            }

            SoundVolume = value;
            PlayerPrefs.SetFloat("SoundVolume", SoundVolume);
        }

        public void MuteMusic(bool isMute)
        {
            MusicPlayer.mute = isMute;
            IsMusicMute = isMute;

            if (isMute)
                PlayerPrefs.SetInt("MuteMusic", 1);
            else
                PlayerPrefs.SetInt("MuteMusic", 0);
        }

        public void MuteSound(bool isMute)
        {
            foreach (AudioSource channel in SoundChannels)
            {
                channel.mute = isMute;
            }

            IsSoundMute = isMute;

            if (isMute)
                PlayerPrefs.SetInt("MuteSound", 1);
            else
                PlayerPrefs.SetInt("MuteSound", 0);
        }

        public void PauseMusic()
        {
            MusicPlayer.Pause();
        }

        public void ResumeMusic()
        {
            MusicPlayer.UnPause();
        }

        public void StopMusic()
        {
            MusicPlayer.Stop();
        }

        public void PauseChannels()
        {
            foreach (AudioSource channel in SoundChannels)
            {
                channel.Pause();
            }
        }

        public void ResumeChannels()
        {
            foreach (AudioSource channel in SoundChannels)
            {
                channel.UnPause();
            }
        }

        public void StopChannels()
        {
            foreach (AudioSource channel in SoundChannels)
            {
                channel.Stop();
            }
        }

    }

}