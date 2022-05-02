using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderDestroyer : MonoBehaviour
{
    public GameModeManager gameMode;
    public UIManager gameUI;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(TagManager.WEAPON))
        {
            AudioManager.AMInstance.PlayGameOverClip();
            gameUI.CanvasGameOver();
            Debug.Log("GameOver...");
            Debug.Log("Weapon Destroyed...");
            gameMode.selectedMode = GameMode.Over;
        }
    }
}
