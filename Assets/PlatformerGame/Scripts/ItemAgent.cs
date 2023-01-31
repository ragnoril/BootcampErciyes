using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace PlatformerGame
{
    public enum ItemType
    {
        Coin,
        Health,
        YellowKey,
        BlueKey,
        GreenKey,
        RedKey
    }

    public class ItemAgent : MonoBehaviour
    {
        public ItemType Type;
        public int Amount;

        public AudioSource SfxPlayer;
        public AudioClip HitSoundClip;

        private void Start()
        {
            transform.DOMove(transform.position + (Vector3.up * 0.25f), 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            SfxPlayer = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                SfxPlayer.PlayOneShot(HitSoundClip);
                Destroy(this.gameObject, 0.2f);
                //Destroy(this.gameObject);                
            }
        }
    }

}