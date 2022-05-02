using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public Text score;
    public Text highscore;
    public Text bullets;

    public static ScoreManager SMInstance { get; private set; }
    private void Awake()
    {
        //If It is SMInstance , and It is not a SMInstance, delete SMInstance
        if (SMInstance != null && SMInstance != this)
        {
            Destroy(this);
        }
        else
        {
            SMInstance = this;
        }
    }

    private void Start()
    {
        //score.text = "00";  // + WeaponRigidBody._SCORE.ToString();     
        highscore.text = PlayerPrefs.GetInt("TScore", 0).ToString();
    }

    private void Update()
    {
        score.text = PlayerWeapon.PWInstance.SCORE.ToString();
        int scoreH = PlayerWeapon.PWInstance.SCORE;
        if(scoreH > PlayerPrefs.GetInt("TScore",0))
        {
            PlayerPrefs.SetInt("TScore",scoreH);
            highscore.text = scoreH.ToString();
        }
    }
    public void GameScore(int score)
    {
        this.score.text = score.ToString();
    }

    public void GameBullet(int bullets)
    {
        this.bullets.text = bullets.ToString();
    }
    
    public void GoToHome()
    {
        AudioManager.AMInstance.PlayButtonClickClip();
        Debug.Log("Goto Home Screen...");
        SceneManager.LoadScene("GameMainMenu");
    }
}
