using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectLevelManager : MonoBehaviour
{
    [Header("Level Icons — kéo level1.png → level8.png")]
    public Sprite[] levelIcons;

    [Header("Level Name Images — kéo img tên level1 → level8")]
    public Sprite[] levelNameSprites;

    [Header("Star counts mặc định mỗi level (1-8 sao)")]
    public int[] starCounts = { 1, 1, 2, 2, 3, 3, 4, 5 };

    [Header("UI References")]
    public Image    imgLevelIcon;
    public Image    imgLevelName;
    public TMP_Text txtStarCount;
    public Image    imgBarFill;
    public TMP_Text txtPercent;
    public Image[]  dots;

    private int _current = 0;
    private const int TOTAL = 8;

    void Start()
    {
        _current = PlayerPrefs.GetInt("LastSelectedLevel", 0);
        Refresh();
    }

    public void OnNextClick()
    {
        SFXManager.Instance?.PlayClick();
        _current = (_current + 1) % TOTAL;
        Refresh();
    }

    public void OnPrevClick()
    {
        SFXManager.Instance?.PlayClick();
        _current = (_current - 1 + TOTAL) % TOTAL;
        Refresh();
    }

    public void OnPlayLevel()
    {
        SFXManager.Instance?.PlayClick();
        PlayerPrefs.SetInt("CurrentLevel", _current);
        PlayerPrefs.SetInt("LastSelectedLevel", _current);
        SceneManager.LoadScene("GameScene");
    }

    public void OnBackClick()
    {
        SFXManager.Instance?.PlayClick();
        SceneManager.LoadScene("MainMenu");
    }

    void Refresh()
    {
        // Icon
        imgLevelIcon.sprite = levelIcons[_current];

        // Tên level (Image sprite)
        imgLevelName.sprite = levelNameSprites[_current];
        imgLevelName.SetNativeSize();

        // Sao — ưu tiên progress đã đạt, fallback về mặc định
        int savedStars = PlayerPrefs.GetInt($"Level{_current}_Stars", 0);
        txtStarCount.text = savedStars > 0
            ? savedStars.ToString()
            : starCounts[_current].ToString();

        // Progress bar
        float best = PlayerPrefs.GetFloat($"Level{_current}_BestPercent", 0f);
        imgBarFill.fillAmount = best / 100f;
        txtPercent.text       = Mathf.RoundToInt(best) + "%";

        // Dots
        for (int i = 0; i < dots.Length; i++)
        {
            Color c = dots[i].color;
            c.a = (i == _current) ? 1f : 0.4f;
            dots[i].color = c;
        }

        PlayerPrefs.SetInt("LastSelectedLevel", _current);
    }
}