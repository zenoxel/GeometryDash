using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    
    [Header("Audio Sources")]
    public AudioSource musicSource;
    
    [Header("Music Clips")]
    public AudioClip mainMenuMusic;
    public AudioClip levelMusic;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    
    void Start()
    {
        // Phát nhạc menu khi vào MainMenu scene
        if (Application.isPlaying && 
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu")
        {
            PlayMainMenu();
        }
    }
    
    public void PlayMainMenu()
    {
        if (musicSource != null && mainMenuMusic != null)
        {
            musicSource.clip = mainMenuMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
    
    public void PlayLevelMusic()
    {
        if (musicSource != null && levelMusic != null)
        {
            musicSource.clip = levelMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
    
    public void StopMusic()
    {
        if (musicSource != null) 
            musicSource.Stop();
    }
    
    public void SetVolume(float vol)
    {
        if (musicSource != null) 
            musicSource.volume = Mathf.Clamp01(vol);
    }
    
    public float GetVolume()
    {
        return musicSource != null ? musicSource.volume : 0f;
    }
}
