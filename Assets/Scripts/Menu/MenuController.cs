using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void OnPlayClick()
    {
        SFXManager.Instance?.PlayClick();
        SceneManager.LoadScene("SelectLevel");
    }

    public void OnCharacterClick()
    {
        SFXManager.Instance?.PlayClick();
        // mở panel chọn nhân vật – bước sau
        Debug.Log("Character panel");
    }

    public void OnMoreGamesClick()
    {
        SFXManager.Instance?.PlayClick();
        Application.OpenURL("https://yourstore.com");
    }

    public void OnRateClick()
    {
        SFXManager.Instance?.PlayClick();
        Application.OpenURL("https://yourstore.com");
    }

    public void OnShareClick()
    {
        SFXManager.Instance?.PlayClick();
        Debug.Log("Share");
    }

    public void OnLeaderboardClick()
    {
        SFXManager.Instance?.PlayClick();
        Debug.Log("Leaderboard");
    }
}