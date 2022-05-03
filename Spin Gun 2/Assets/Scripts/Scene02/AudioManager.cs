using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource = null;

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
    public void PlayAudio(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
