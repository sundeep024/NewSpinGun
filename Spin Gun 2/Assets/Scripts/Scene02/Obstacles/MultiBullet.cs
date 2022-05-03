using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBullet : MonoBehaviour
{
    [SerializeField] private AudioClip _multiBulletCollectSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.WEAPON))
        {
            PlayerWeapon.BULLETCOUNT += 10;
            GamePlayCanvas.GPCInstance.GameBullet(PlayerWeapon.BULLETCOUNT);
            Debug.Log("Multi Bullet Collect" + PlayerWeapon.BULLETCOUNT);
            AudioManager.AMInstance.PlayAudio(_multiBulletCollectSound);
            Destroy(gameObject);
        }
    }
}
