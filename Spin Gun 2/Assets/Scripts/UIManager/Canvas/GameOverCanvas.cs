using UnityEngine;
using UnityEngine.UI;

public class GameOverCanvas : MonoBehaviour
{
    public UIManager gameUI;

    [SerializeField] private Button _btnRestart;
    [SerializeField] private Button _btnMainMenu;

    public Text score;
    public Text highscore;
    public int Hscore;

    public static GameOverCanvas GOCInstance { get; private set; } 
    [SerializeField] private AudioClip _btnClickSound;

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
        GameModeManager.OnGameOver?.Invoke();
        GameModeManager.selectedMode = GameMode.Over;
    }

    public void UpdateScore(int Tscore)
    {
        Debug.Log("Upfate Method Score .." + Tscore);
        score.text = Tscore.ToString();
        Debug.Log("High Score Update  Value" + score.text);

        HighScore(Tscore);
    }
    public void HighScore(int Hscore)
    {
        int scoreH = Hscore;
        if (scoreH > PlayerPrefs.GetInt("HScore",0))
        {
            PlayerPrefs.SetInt("HScore",scoreH);
            highscore.text = scoreH.ToString();
        }
    }

    public void MainMenu()
    {
        AudioManager.AMInstance.PlayAudio(_btnClickSound);
        //GameOver();
        GamePlayCanvas.GPCInstance.score.text = "00";
        GamePlayCanvas.GPCInstance.bullets.text = "20";
        gameUI.CanvasGameMainMenu();
        GameModeManager.selectedMode = GameMode.MainMenu;
    }

    public void GameRestart()
    {
        GamePlayCanvas.GPCInstance.score.text = "00";
        GamePlayCanvas.GPCInstance.bullets.text = "20";
        
        AudioManager.AMInstance.PlayAudio(_btnClickSound);
        PlayerWeapon.PWInstance._weaponRD.position = Vector2.zero;
        gameUI.CanvasGamePlay();
        GameModeManager.selectedMode = GameMode.Play;
        Debug.Log("GameRestart!..");
    }
    public void OnDisable()
    {
        GameModeManager.OnScoreChange -= UpdateScore;
    }
}
