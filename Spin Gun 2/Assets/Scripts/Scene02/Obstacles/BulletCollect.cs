using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollect : MonoBehaviour
{
    [SerializeField] private AudioClip _sglBulletCollectSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(TagManager.WEAPON))
        {
            PlayerWeapon.BULLETCOUNT += 5;
            GamePlayCanvas.GPCInstance.GameBullet(PlayerWeapon.BULLETCOUNT);
            Debug.Log("Single Bullet Collect" + PlayerWeapon.BULLETCOUNT);
            AudioManager.AMInstance.PlayAudio(_sglBulletCollectSound);
            Destroy(gameObject);
        }
    }
}
