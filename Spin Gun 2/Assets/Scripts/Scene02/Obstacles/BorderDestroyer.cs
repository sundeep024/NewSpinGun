using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderDestroyer : MonoBehaviour
{
    public UIManager gameUI;
    [SerializeField] private AudioClip _gameOverSound; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(TagManager.WEAPON))
        {
            AudioManager.AMInstance.PlayAudio(_gameOverSound);
            gameUI.CanvasGameOver();
            Debug.Log("GameOver...");
            Debug.Log("Weapon Destroyed...");
            GameModeManager.selectedMode = GameMode.Over;
        }
    }
}
