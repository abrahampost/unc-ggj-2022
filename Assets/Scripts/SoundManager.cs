using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource idleAudio;
    void Start() {
        DontDestroyOnLoad(this);
        idleAudio.Play();
    }
    
    public AudioSource jumpAudio;
    public void PlayJump()
    {
        jumpAudio.Play();
    }

    public AudioSource shootGunAudio;
    public void ShootGun()
    {
        shootGunAudio.Play();
    }

    public AudioSource shootingTelegunAudio;
    public void ShootingTelegun()
    {
        shootingTelegunAudio.Play();
    }

    public void ShootingTelegunStop()
    {
        shootingTelegunAudio.Stop();
    }
    public AudioSource teleportingTelegunAudio;
    public void TeleportingTelegun()
    {
        teleportingTelegunAudio.Play();
    }

    public AudioSource jetpackAudio;
    public void Jetpack()
    {
        jetpackAudio.Play();
    }
    public void JetpackStop()
    {
        jetpackAudio.Stop();
    }

    public AudioSource changeDimensionAudio;
    public void ChangeDimension()
    {
        changeDimensionAudio.Play();
    }
}
