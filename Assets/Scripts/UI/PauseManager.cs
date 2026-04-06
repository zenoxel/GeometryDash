using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public Image      imgBarBest;
    public TMP_Text   txtBestPercent;
    public Image      imgBarNow;
    public TMP_Text   txtNowPercent;
    public Toggle     toggleMusic;
    public Toggle     toggleAutoRetry;

    public void OnPauseClick()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        RefreshUI();
    }

    public void OnResumeClick()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void OnBackClick()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("SelectLevel");
    }

    void RefreshUI()
    {
        int   lvl  = PlayerPrefs.GetInt("CurrentLevel", 0);
        float best = PlayerPrefs.GetFloat($"Level{lvl}_BestPercent", 0f);
        float now  = GameManager.Instance?.CurrentProgress ?? 0f;

        imgBarBest.fillAmount = best / 100f;
        txtBestPercent.text   = Mathf.RoundToInt(best) + "%";
        imgBarNow.fillAmount  = now / 100f;
        txtNowPercent.text    = Mathf.RoundToInt(now) + "%";
    }
}