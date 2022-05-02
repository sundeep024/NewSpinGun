using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas GameMainMenu;
    public Canvas GamePlaying;
    public Canvas GameOver;

    public GameModeManager gameMode;

    public GameObject playerWeapon;
    public GameObject spawnObject;

    public Transform weaponPos;
    //public Canvas GamePause;


    // Start is called before the first frame update
    void Start()
    {        
        gameMode.OnGameMainMenu += CanvasGameMainMenu;
        gameMode.OnGamePlay += CanvasGamePlay;
        gameMode.OnGameOver += CanvasGameOver;
        //gameMode.OnGamePause += CanvasGamePause;
    }
    
    public void CanvasGameMainMenu()
    {
        GameMainMenu.enabled = true;
        GamePlaying.enabled = false;
        GameOver.enabled = false;

        PlayerWeapon.PWInstance._weaponRD.isKinematic =true;
        weaponPos.position = PlayerWeapon.PWInstance._weaponRD.position;
        PlayerWeapon.PWInstance.BULLETCOUNT = 00;
        CollectObject.COInstance.StopCoroutine("ReSpawningObjects");
        GameOverCanvas.GOCInstance.score.text = "00";
    }

    public void CanvasGamePlay()
    {
        GameMainMenu.enabled = false;
        GamePlaying.enabled = true;
        GameOver.enabled = false;


        PlayerWeapon.PWInstance._weaponRD.isKinematic = false;
        PlayerWeapon.PWInstance.BULLETCOUNT = 20;
        PlayerWeapon.PWInstance.SCORE = 00;
        PlayerWeapon.PWInstance.StartCoroutine("GunRotate");
        CollectObject.COInstance.StartCoroutine("ReSpawningObjects");
        PlayerWeapon.PWInstance._weaponRD.constraints = RigidbodyConstraints2D.None;
        //GamePause.enabled = false;
        Debug.Log("GamePlay Canvas Method Call");

    }

    public void CanvasGameOver()
    {
        GameMainMenu.enabled = false;
        GamePlaying.enabled = false;
        GameOver.enabled = true;

        CollectObject.COInstance.StopCoroutine("ReSpawningObjects");
        
        PlayerWeapon.PWInstance.StopCoroutine("GunRotate");

        
        PlayerWeapon.PWInstance._weaponRD.position =new Vector2(0,2);

        Debug.Log("Weapon Position" + weaponPos.position);
        Debug.Log("Weapon RigidBody Position" + PlayerWeapon.PWInstance._weaponRD.position);
        PlayerWeapon.PWInstance._weaponRD.isKinematic = true;
        PlayerWeapon.PWInstance.BULLETCOUNT = 0;
        //weapon._weaponRD.position = weaponPosition.position;

        //PlayerWeapon.WRBInstance._weaponRD.gravityScale = 0;
        PlayerWeapon.PWInstance._weaponRD.constraints = RigidbodyConstraints2D.FreezeAll;
        //Game_Over_Weapon_Position();
        //GamePause.enabled = false;
    }

    private void OnDisable()
    {
        gameMode.OnGameMainMenu -= CanvasGameMainMenu;
        gameMode.OnGamePlay -= CanvasGamePlay;
        gameMode.OnGameOver -= CanvasGameOver;
    }
    
}
    