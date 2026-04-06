using UnityEngine;

public class IconManager : MonoBehaviour
{
    public static IconManager Instance;
    
    public int selectedIconIndex = 0; // Icon đang được chọn
    
    void Awake()
    {
        // Singleton - tồn tại xuyên scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        // Load icon đã lưu trước đó
        selectedIconIndex = PlayerPrefs.GetInt("SelectedIcon", 0);
    }
    
    public void SelectIcon(int index)
    {
        selectedIconIndex = index;
        PlayerPrefs.SetInt("SelectedIcon", index);
        PlayerPrefs.Save();
    }
}