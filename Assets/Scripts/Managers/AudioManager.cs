using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource saveSlotSound;
    [SerializeField] AudioSource backButtonSound;
    [SerializeField] AudioSource okButtonSound;
    [SerializeField] AudioSource chaChingButtonSound;
    [SerializeField] AudioSource rightAndLeftButtonSound;
    [SerializeField] AudioSource cancelButtonSound;

    public void PlaySaveSlotSound()
    {
        saveSlotSound.Play();
    }

    public void PlayBackButtonSound()
    {
        backButtonSound.Play();
    }

    public void PlayOKButtonSound()
    {
        okButtonSound.Play();
    }

    public void PlayChaChingButtonSound()
    {
        chaChingButtonSound.Play();
    }

    public void PlayRightAndLeftButtonSound()
    {
        rightAndLeftButtonSound.Play();
    }

    public void PlayCancelButtonSound()
    {
        cancelButtonSound.Play();
    }
}
