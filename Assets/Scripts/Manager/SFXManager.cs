using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;
    public AudioSource sfxSource;
    public AudioClip clickSound;   // kéo clickbtn.mp3 vào
    public AudioClip explodeSound;
    public AudioClip jumpSound;
    public AudioClip winSound;

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);
    }

    public void PlayClick() => sfxSource.PlayOneShot(clickSound);
    public void PlayExplode() => sfxSource.PlayOneShot(explodeSound);
    public void PlayJump()    => sfxSource.PlayOneShot(jumpSound);
    public void PlayWin()     => sfxSource.PlayOneShot(winSound);
}