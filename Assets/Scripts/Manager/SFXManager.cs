using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;
    public AudioSource sfxSource;
    public AudioClip clickSound;   // kéo clickbtn.mp3 vào

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);
    }

    public void PlayClick() => sfxSource.PlayOneShot(clickSound);
}