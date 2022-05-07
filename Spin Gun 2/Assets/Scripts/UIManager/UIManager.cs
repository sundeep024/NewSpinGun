using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas GameMainMenu;
    public Canvas GamePlaying;
    public Canvas GameOver;


    public Transform weaponPos;
    //public Canvas GamePause;


    // Start is called before the first frame update
    void Start()
    {        
        GameModeManager.OnGameMainMenu += CanvasGameMainMenu;
        GameModeManager.OnGamePlay += CanvasGamePlay;
        GameModeManager.OnGameOver += CanvasGameOver;
        //gameMode.OnGamePause += CanvasGamePause;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {

        }
    }
    public void CanvasGameMainMenu()
    {
        GameMainMenu.enabled = true;
        GamePlaying.enabled = false;
        GameOver.enabled = false;

        PlayerWeapon.PWInstance._weaponRD.isKinematic =true;
        weaponPos.position = PlayerWeapon.PWInstance._weaponRD.position;
        PlayerWeapon.BULLETCOUNT = 00;
        //PlayerWeapon.PWInstance.StopCoroutine("WeaponShooting");
        PlayerWeapon.PWInstance.StartCoroutine("GunRotate");
        CollectObject.COInstance.StopCoroutine("ReSpawningObjects");
        GameOverCanvas.GOCInstance.score.text = "00";
    }

    public void CanvasGamePlay()
    {
        GameMainMenu.enabled = false;
        GamePlaying.enabled = true;
        GameOver.enabled = false;

        PlayerWeapon.PWInstance._weaponRD.isKinematic = false;
        weaponPos.position = PlayerWeapon.PWInstance._weaponRD.position;
        PlayerWeapon.PWInstance._weaponRD.transform.rotation = Quaternion.Euler(0,0,-90);
        PlayerWeapon.BULLETCOUNT = 20;
        PlayerWeapon.SCORE = 00;
        PlayerWeapon.PWInstance.StopCoroutine("GunRotate");
        //PlayerWeapon.PWInstance.GunRotation();
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
        //PlayerWeapon.PWInstance.StopCoroutine("WeaponShooting");
        //weaponPos.position = PlayerWeapon.PWInstance._weaponRD.position;
        CollectObject.COInstance.StopCoroutine("ReSpawningObjects");        
        
        PlayerWeapon.PWInstance.StopCoroutine("GunRotate");
        
        PlayerWeapon.PWInstance._weaponRD.position =new Vector2(0,2);
        weaponPos.position = PlayerWeapon.PWInstance._weaponRD.position;

        /*
        Debug.Log("Weapon Position" + weaponPos.position);
        Debug.Log("Weapon RigidBody Position" + PlayerWeapon.PWInstance._weaponRD.position);*/
        PlayerWeapon.PWInstance._weaponRD.isKinematic = true;
        PlayerWeapon.BULLETCOUNT = 0;
        PlayerWeapon.PWInstance._weaponRD.constraints = RigidbodyConstraints2D.FreezeAll;        
    }

    private void OnDisable()
    {
        GameModeManager.OnGameMainMenu -= CanvasGameMainMenu;
        GameModeManager.OnGamePlay -= CanvasGamePlay;
        GameModeManager.OnGameOver -= CanvasGameOver;
    }
    
}
    