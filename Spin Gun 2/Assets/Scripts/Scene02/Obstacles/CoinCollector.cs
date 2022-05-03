using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private AudioClip _coinCollectSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(TagManager.WEAPON))
        {
            Destroy(gameObject);
            PlayerWeapon.SCORE += 5;
            GameModeManager.OnScoreChange?.Invoke(PlayerWeapon.SCORE);
            Debug.Log("Update Collision Score .." + PlayerWeapon.SCORE);
            Debug.Log("Subscribed OnScoreChange Action");
            GamePlayCanvas.GPCInstance.GameScore(PlayerWeapon.SCORE);
            AudioManager.AMInstance.PlayAudio(_coinCollectSound);
            Debug.Log("Weapon Collect Coin");
        }
    }
}
