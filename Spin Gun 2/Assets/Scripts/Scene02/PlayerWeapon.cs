using System.Collections;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    //Weapon Variables...
    [SerializeField] public Rigidbody2D _weaponRD;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Vector3 _rotate;
    private bool _isRotating=true;
    [SerializeField] private float _bulletForce = 50;

    [SerializeField] float torque;

    //Weapon Bullets variables...
    [SerializeField] private Rigidbody2D _bullet;
    [SerializeField] private Transform _SpawnPoint;
    public GameObject _spawnHolder;
    [SerializeField] private GameObject _muzzleFlash;
    public int flashTime;
    bool isFlashing = false;


    //Game Score  and Bullet count Variables...
    public int BULLETCOUNT;
    public int SCORE;
    

    public Transform weaponRightPosition;
    public Transform weaponLeftPosition;

    public GameModeManager gameMode;
    public UIManager gameUI;
    //Vector2 screenBounds; 

    float posX = 2.3f;

    [Header("AudioClip")]
    [SerializeField] private AudioClip redZoneClip;

    public static PlayerWeapon PWInstance { get; private set; }

    private void Awake()
    {
        if(PWInstance != null && PWInstance != this)
        {
            Destroy(this);
        }
        else
        {
            PWInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //BULLETCOUNT = 20;
        _muzzleFlash.SetActive(false);
    }
    
    private void Update()
    {
        //When User tounch on screen or press mouse button
        if (Input.GetMouseButtonDown(0))
        { 
            if (BULLETCOUNT <= 0)    // when bullet zero than game over
            {
                BULLETCOUNT = 0;
                Debug.Log("GameOver..");

                //gameMode.selectedMode = GameMode.Over;
            }
            else
            {
                GunShoot();     // otherwise user can shoot 
            }
        }
        WeaponScreenWidthBound();

        //WeaponForce();  // weapon force to opposite side when user fire
    }
    
   
    public void GunShoot()
    {            
        //AudioManager.PlayGunFireClip(); // play sound when fire
        AudioManager.AMInstance.PlayGunFireClip();

        //bullet instantiate, set bullet position and rotation of bullet 
        Rigidbody2D bulletRD = Instantiate(_bullet, _SpawnPoint.position, Quaternion.identity);

        //bulletRD.velocity = _SpawnPoint.right* _bulletForce;

        //bullet fire on right side of gun point and force
        bulletRD.AddForce(_SpawnPoint.right * _bulletForce, ForceMode2D.Impulse);

       // _weaponRD.AddTorque(rotateSpeed, ForceMode2D.Force);
        //Instantiate bullet set patent  
        bulletRD.transform.SetParent(_spawnHolder.transform);

        //_weaponRD.AddTorque(10, ForceMode2D.Force);
        WeaponForce();
        isFlashing = false;
        if (!isFlashing)
        {
            StartCoroutine(MuzzleFlashfx());
        }
        //GunRotation();
        //user fire bullet and descrease bullet count
        BULLETCOUNT--;
        GamePlayCanvas.GPCInstance.GameBullet(BULLETCOUNT);
        //ScoreManager.SMInstance.GameBullet(BULLETCOUNT);
    }

    public void WeaponForce()
    {
        AudioManager.AMInstance.PlayGunBackForceClip();
        //when a gun fire than weapon force to opposite side
        _weaponRD.AddForce(-_weaponRD.transform.right * 1f, ForceMode2D.Impulse);           
    }

    public void WeaponScreenWidthBound()
    {
        //yield return new WaitForSeconds(0.05f);
        if (_weaponRD.position.x > posX)
        {
            //Debug.Log("Weapon X Value" + _weaponRD.position.x);
            _weaponRD.position = weaponRightPosition.position;
        }
        if (_weaponRD.position.x < -posX)
        {
            _weaponRD.position = weaponLeftPosition.position;
        }
    }
    
    IEnumerator MuzzleFlashfx()
    {
        _muzzleFlash.SetActive(true);
        var flash = 0;
        isFlashing = true;
        while(flash <= flashTime)
        {
            flash++;
            yield return null;
        }
        _muzzleFlash.SetActive(false);
        isFlashing = false;
    }

    /*
    public void GunRotation()
    {
        //float h = Input.GetAxis("Mouse X")* torque * Time.deltaTime;
        //_weaponRD.AddTorque(torque , ForceMode2D.Force);
        Vector2 up = Vector2.up;
        _weaponRD.AddTorque(-Input.GetAxis("Mouse X") * torque * up)
    }*/
    
    IEnumerator GunRotate()
    {
         while (_isRotating)
         {
             yield return new WaitForSeconds(0.02f);
             //_weaponRD.AddTorque(rotateSpeed, ForceMode2D.Force);
             transform.Rotate(_rotate * rotateSpeed * Time.deltaTime, Space.Self);
         }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.gameObject.CompareTag(TagManager.BULLET))  // weapon collect bullet increase score plus 5
        {
            AudioManager.AMInstance.PlayGunPickUpClip();
            BULLETCOUNT = BULLETCOUNT + 5;
            GamePlayCanvas.GPCInstance.GameBullet(BULLETCOUNT);

            //ScoreManager.SMInstance.GameBullet(BULLETCOUNT);
            Debug.Log("bullet is " + BULLETCOUNT);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag(TagManager.INCREASE_BULLET))  // weapon collect multibullet increase score plus 10
        {
            AudioManager.AMInstance.PlayGunPickUpClip();
            BULLETCOUNT = BULLETCOUNT + 10;
            GamePlayCanvas.GPCInstance.GameBullet(BULLETCOUNT);

            //ScoreManager.SMInstance.GameBullet(BULLETCOUNT);
            Debug.Log("multi buleet is " + BULLETCOUNT);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag(TagManager.COIN_COLLECTOR))   // weapon collect coin increase score plus 5
        {
            Destroy(collision.gameObject);
            SCORE = SCORE + 5;            

            GameModeManager.OnScoreChange?.Invoke(SCORE);
            Debug.Log("Upfate Collision Score .." + SCORE);
            //GameOverCanvas.GOCInstance.UpdateScore(SCORE);
            Debug.Log("Subscribed OnScoreChange Action");
            GamePlayCanvas.GPCInstance.GameScore(SCORE);
            AudioManager.AMInstance.PlayCoinCollectClip();
            Debug.Log("Score is " + SCORE);
        }
        else if(collision.gameObject.CompareTag(TagManager.RED_ZONE))
        {
            //AudioManager.AMInstance.PlayRedZoneClip();
            AudioManager.AMInstance.PlayAudio(redZoneClip);
            IncreasedWeaponForce();
            Debug.Log("Weapon collide with RedZone");
            Destroy(collision.gameObject);
        }       

    }

    public void IncreasedWeaponForce()
    {
        _weaponRD.AddForce(-_weaponRD.transform.right * 2.2f, ForceMode2D.Impulse);
    }

    
}
