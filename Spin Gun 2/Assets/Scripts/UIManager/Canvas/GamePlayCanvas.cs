using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class GamePlayCanvas : MonoBehaviour
{
    public Button btnHome;
    public Button btnPause;
    public Text bullets;
    public Text score;
    //public Button btnPause;

    private UIManager gameUI;
    public Transform weaponPosition;

    public static GamePlayCanvas GPCInstance { get; private set; }


    private void Awake()
    {
        if (GPCInstance != null && GPCInstance != this)
        {
            Destroy(this);
        }
        else
        {
            GPCInstance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //score.text = "00";
        //bullets.text = "20";
        gameUI = GetComponent<UIManager>();
        btnHome.onClick.AddListener(MainMenu);
        btnPause.onClick.AddListener(PauseGame);
        //btnPause.onClick.AddListener(GamePause);



    }

    public void MainMenu()
    {
        AudioManagerScene01.PlayButtonClickClip();
        gameUI.CanvasGameMainMenu();
        ShowMainMenuBox();   
    }

    public void GameBullet(int bullets)
    {
        this.bullets.text = bullets.ToString();
    }

    public void GameScore(int score)
    {
        this.score.text = score.ToString();
    }

   /* public void PauseGame()
    {
        Debug.Log("Game Pause");
    }*/
    public void ShowMainMenuBox()
    {
        bool showBox = EditorUtility.DisplayDialog(
            title: "Main Menu !",
            message: "Are You Sure Want To Quit Level !",
            ok: "Yes",
            cancel: "No"
            );

        if (showBox)
        {
            gameUI.CanvasGameMainMenu();
            Debug.Log("Quit Level");
        }
        else
        {
            Debug.Log("Continue To Play Game !");
        }
    }

    public void PauseGame()
    {
        bool showBox = EditorUtility.DisplayDialog(
            title: "Game Pause!",
            message: "Game Pause !",
            ok: "Resume",
            cancel: "Exit"
            );

        if (showBox)
        {
           // gameUI.CanvasGameMainMenu();
            Debug.Log("Quit Level");
        }
        else
        {
            Debug.Log("Continue To Play Game !");
        }
    }
}



/*public void GamePause()
{
    AudioManager.AMInstance.PlayButtonClickClip();
    ShowGamePauseBox();

}
public void ShowGamePauseBox()
{
    bool showBox = EditorUtility.DisplayDialog(
        title: "Paused !",
        message: "Game Paused!",
        ok: "Yes",
        cancel: "No"
        );

    if (showBox)
    {
        gameUI.CanvasGameMainMenu();
        Debug.Log("Quit Level");
    }
    else
    {
        Debug.Log("Continue To Play Game !");
    }
}*/