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
    public static int BULLETCOUNT;
    public static int SCORE;
    

    public Transform weaponRightPosition;
    public Transform weaponLeftPosition;

    public UIManager gameUI;
    //Vector2 screenBounds; 

    float posX = 2.3f;

    [Header("AudioClip")]
    [SerializeField] private AudioClip _gunFireSound;
    [SerializeField] private AudioClip _gunBackForceSound;

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
        //GameModeManager.OnGameShoot += WeaponShooting;
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
        //GunRotation();
        StartCoroutine(GunRotation());

        // play sound when fire
        AudioManager.AMInstance.PlayAudio(_gunFireSound);

        //bullet instantiate, set bullet position and rotation of bullet 
        Rigidbody2D bulletRD = Instantiate(_bullet, _SpawnPoint.position, Quaternion.identity);

        //bulletRD.velocity = _SpawnPoint.right* _bulletForce;

        //bullet fire on right side of gun point and force
        bulletRD.AddForce(_SpawnPoint.right * _bulletForce, ForceMode2D.Impulse);

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

        StopCoroutine(GunRotation());
    }

    public void WeaponForce()
    {
        AudioManager.AMInstance.PlayAudio(_gunBackForceSound);

        //when a gun fire than weapon force to opposite side
        _weaponRD.AddForce(-_weaponRD.transform.right * 2.2f, ForceMode2D.Impulse);           
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

    public IEnumerator GunRotation()
    {
        _weaponRD.AddTorque(5.0f, ForceMode2D.Force);//, ForceMode2D.Impulse);
        yield return null;
    }
     
    /*public void GunRotation()
    {
        //float h = Input.GetAxis("Mouse X")* torque * Time.deltaTime;
        //_weaponRD.AddTorque(torque , ForceMode2D.Force);
        //Vector2 up = Vector2.up;
        _weaponRD.AddTorque(torque * 0.2f, ForceMode2D.Impulse);
    }
    */

    
    IEnumerator GunRotate()
    {
         while (_isRotating)
         {
             yield return new WaitForSeconds(0.02f);
             //_weaponRD.AddTorque(rotateSpeed, ForceMode2D.Force);
             transform.Rotate(_rotate * rotateSpeed * Time.deltaTime, Space.Self);
         }
    }


    public void IncreasedWeaponForce()
    {
        _weaponRD.AddForce(-_weaponRD.transform.right * 2.2f, ForceMode2D.Impulse);
    }

}
