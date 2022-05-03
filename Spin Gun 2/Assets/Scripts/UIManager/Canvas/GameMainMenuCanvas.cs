using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class GameMainMenuCanvas : MonoBehaviour
{
    public Button btnPlay;
    public Button btnQuit;
    public UIManager gameUI;

    [SerializeField] private AudioClip _btnClickSound;

    // Start is called before the first frame update
    void Start()
    {        
        Invoke(nameof(ShowDialogue), 0.5f);
        btnPlay.onClick.AddListener(GamePlay);
        btnQuit.onClick.AddListener(GameQuit);
        Debug.Log("Weapon Kinematic");
        gameUI.CanvasGameMainMenu();
    }

    public void GamePlay()
    {
        /*if(gameMode.OnGamePlay != null)
        {
            gameMode.OnGamePlay();
        }*/

        AudioManager.AMInstance.PlayAudio(_btnClickSound);
        GameModeManager.OnGamePlay?.Invoke();
        GameModeManager.selectedMode = GameMode.Play;
        gameUI.CanvasGamePlay();
        //PlayerWeapon.WRBInstance.WeaponMovement();
        //PlayerWeapon.WRBInstance.StartCoroutine("WeaponMove");

        Debug.Log("GamePlay Canvas Method Call");

    }

    public void GameQuit()
    {
        AudioManager.AMInstance.PlayAudio(_btnClickSound);
        Application.Quit();
        ShowExitDialogueBox();
    }
    
    public void ShowDialogue()
    {
        bool showBox = EditorUtility.DisplayDialog(
            title: "Welcome to Spin The Gun !",
            message: "Play the Spin Gun !",
            ok: "Yes",
            cancel: "No"
            );

        if (showBox)
        {
            Debug.Log("Playing Game");
        }
        else
        {
            Debug.Log("Default Action...");
        }
    }

    public void ShowExitDialogueBox()
    {
        bool showBox = EditorUtility.DisplayDialog(
            title: "Exit Game !",
            message: "Are You Sure Want To Exit Game !",
            ok: "Yes",
            cancel: "No"
            );

        if(showBox)
        {
            Application.Quit();
            Debug.Log("Game Quit");
        }
        else
        {
            Debug.Log("Continue To Play Game !");
        }
    }
}
