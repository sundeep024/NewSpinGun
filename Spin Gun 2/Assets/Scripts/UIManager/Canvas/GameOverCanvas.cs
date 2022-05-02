using UnityEngine;
using UnityEngine.UI;

public class GameOverCanvas : MonoBehaviour
{
    public GameModeManager gameMode;
    public UIManager gameUI;

    [SerializeField] private Button _btnRestart;
    [SerializeField] private Button _btnMainMenu;
    public static GameOverCanvas GOCInstance { get; private set; } 

    public Text score;
    public Text highscore;
    public int Hscore;


    private void Awake()
    {
        if(GOCInstance !=null && GOCInstance != this)
        {
            Destroy(this);
        }
        else
        {
            GOCInstance = this;
        }
    }

    private void Start()
    {
        GameModeManager.OnScoreChange += UpdateScore;
        highscore.text = PlayerPrefs.GetInt("HScore",0).ToString();
        _btnRestart.onClick.AddListener(GameRestart);
        _btnMainMenu.onClick.AddListener(MainMenu);
    }

   
    public void GameOver()
    {
       /* if(gameMode.OnGameOver != null)
        {
            gameMode.OnGameOver();
        }*/
        gameMode.OnGameOver?.Invoke();
        gameMode.selectedMode = GameMode.Over;
    }

    public void UpdateScore(int Tscore)
    {
        Debug.Log("Upfate Method Score .." + Tscore);
        score.text = Tscore.ToString();
        Debug.Log("High Score Update  Value" + score.text);

        HighScore(Tscore);
        //GameModeManager.OnScoreChange -= UpdateScore;
        //Hscore = score;        
    }
    public void HighScore(int Hscore)
    {
        //this.score.text = PlayerWeapon.WRBInstance.SCORE.ToString();
        int scoreH = Hscore;
        //Debug.Log("High Score Value" + scoreH);
        //this.score.text = scoreH.ToString();
        if (scoreH > PlayerPrefs.GetInt("HScore",0))
        {
            PlayerPrefs.SetInt("HScore",scoreH);
            highscore.text = scoreH.ToString();
        }
    }

    public void MainMenu()
    {
        AudioManager.AMInstance.PlayButtonClickClip();
        //GameOver();
        gameUI.CanvasGameMainMenu();
        gameMode.selectedMode = GameMode.MainMenu;
    }

    public void GameRestart()
    {
        GamePlayCanvas.GPCInstance.score.text = "00";
        GamePlayCanvas.GPCInstance.bullets.text = "20";
        AudioManager.AMInstance.PlayButtonClickClip();
        PlayerWeapon.PWInstance._weaponRD.position = Vector2.zero;
        gameUI.CanvasGamePlay();
        gameMode.selectedMode = GameMode.Play;
        //WeaponRigidBody.WRBInstance._weaponRD.position = weaponPosition.position;
        Debug.Log("GameRestart!..");
    }
    public void OnDisable()
    {
        GameModeManager.OnScoreChange -= UpdateScore;
    }
}
