using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace PlatformerGame
{
    public class AudioSettings : MonoBehaviour
    {
        public AudioMixer MusicMixer;
        public AudioMixer SfxMixer;

        public void ChangeMusicVolume(float value)
        {
            MusicMixer.SetFloat("musicVol", value);
        }

        public void ChangeSfxVolume(float value)
        {
            SfxMixer.SetFloat("musicVol", value);
        }
    }
}
