using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public LevelLoader      levelLoader;
    public PlayerController player;

    [HideInInspector] public float CurrentProgress = 0f;
    [HideInInspector] public bool  IsPlaying       = false;

    private int   _levelIndex;
    private float _levelEndX;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    void Start()
    {
        _levelIndex = PlayerPrefs.GetInt("CurrentLevel", 0) + 1;
        StartLevel();
    }

    public void StartLevel()
    {
        IsPlaying       = true;
        CurrentProgress = 0f;
        levelLoader.LoadLevel(_levelIndex);
        player.ResetPlayer();
    }

    public void SetLevelEndX(float x) => _levelEndX = x;

    public void OnPlayerDied()
    {
        if (!IsPlaying) return;
        IsPlaying = false;
        SFXManager.Instance?.PlayExplode();
        Invoke(nameof(StartLevel), 1.2f);
    }

    public void OnPlayerWin()
    {
        if (!IsPlaying) return;
        IsPlaying = false;
        int lvl = _levelIndex - 1;
        PlayerPrefs.SetInt($"Level{lvl}_Completed", 1);
        PlayerPrefs.SetFloat($"Level{lvl}_BestPercent", 100f);
        Invoke(nameof(BackToSelect), 2f);
    }

    public void BackToSelect() =>
        SceneManager.LoadScene("SelectLevel");
}