using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedZone : MonoBehaviour
{
    [SerializeField] private AudioClip redZoneClip;
    // weapon touch redZone and add force to weapon
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.WEAPON))
        {
            //AudioManager.AMInstance.PlayRedZoneClip();
            AudioManager.AMInstance.PlayAudio(redZoneClip);
            PlayerWeapon.PWInstance.IncreasedWeaponForce();
            //gameUI.CanvasGameOver();
            Debug.Log("Weapon collide with RedZone");
        }
    }
}
