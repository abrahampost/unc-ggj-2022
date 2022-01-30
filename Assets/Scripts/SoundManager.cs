using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static bool instantiated = false;
    public AudioSource idleAudio;
    void Start() {
        if (instantiated) {
            Destroy(this);
            return;
        }
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
