using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip coinCollectClip = null;
    [SerializeField] private AudioClip gunFireClip = null;
    [SerializeField] private AudioClip gunBackForceClip = null;
    [SerializeField] private AudioClip gunPickUpClip = null;
    [SerializeField] private AudioClip gameOverClip = null;
    [SerializeField] private AudioClip redZoneClip = null;
    [SerializeField] private AudioClip buttonClickClip = null;


    public static AudioManager AMInstance { get; private set; }

    private void Awake()
    {
        //If It is AMInstance , and It is not a AMInstance, delete AMInstance
        if(AMInstance != null  && AMInstance != this)
        {
            Destroy(this);
        }
        else
        {
            AMInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayCoinCollectClip()
    {
        audioSource.PlayOneShot(coinCollectClip);
    }   

    public void PlayGunFireClip()
    {
        audioSource.PlayOneShot(gunFireClip);
    }

    public void PlayGunBackForceClip()
    {
        audioSource.PlayOneShot(gunBackForceClip);
    }
    public void PlayGunPickUpClip()
    {
        audioSource.PlayOneShot(gunPickUpClip);
    }
    public void PlayGameOverClip()
    {
        audioSource.PlayOneShot(gameOverClip);
    }
    /*public void PlayRedZoneClip()
    {
        audioSource.PlayOneShot(redZoneClip);
    }
    */
    public void PlayButtonClickClip()
    {
        audioSource.PlayOneShot(buttonClickClip);
    }

    public void PlayAudio(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
