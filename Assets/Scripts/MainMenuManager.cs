using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Nút Play
    public void OnPlayButton()
    {
        SceneManager.LoadScene("SelectLevel");
    }
    
    // Nút chọn nhân vật (icon mặt cười)
    public void OnSelectIconButton()
    {
        SceneManager.LoadScene("SelectIcon");
    }
    
    // Nút More Games
    public void OnMoreGamesButton()
    {
        Application.OpenURL("https://play.google.com"); // thay link của bạn
    }
}