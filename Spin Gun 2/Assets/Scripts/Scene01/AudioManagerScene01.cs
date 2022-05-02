using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScene01 : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip buttonClickClip = null;

    private static AudioSource _audioSource = null;
    private static AudioClip _buttonClickClip = null;

    // Start is called before the first frame update
    void Start()
    {
        PlayAudio();
    }

    public void PlayAudio()
    {
        _audioSource = audioSource;
        _buttonClickClip = buttonClickClip;
    }

    public static void PlayButtonClickClip()
    {
        _audioSource.PlayOneShot(_buttonClickClip);
    }
}
