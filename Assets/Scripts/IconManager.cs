using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
    public static IconManager Instance;
    
    public int selectedIconIndex = 0;
    public Sprite[] iconSprites;
    private Sprite _selectedSprite;
    
    public Sprite SelectedSprite
    {
        get
        {
            if (_selectedSprite == null && iconSprites != null && iconSprites.Length > 0)
            {
                _selectedSprite = iconSprites[selectedIconIndex];
            }
            return _selectedSprite;
        }
    }
    
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
        }
        
        selectedIconIndex = PlayerPrefs.GetInt("SelectedIcon", 0);
    }
    
    public void SelectIcon(int index, Sprite selectedSprite)
    {
        selectedIconIndex = index;
        _selectedSprite = selectedSprite;
        PlayerPrefs.SetInt("SelectedIcon", index);
        PlayerPrefs.Save();
    }
    
    public void SetIconSprites(Sprite[] sprites)
    {
        iconSprites = sprites;
        if (iconSprites != null && iconSprites.Length > selectedIconIndex)
        {
            _selectedSprite = iconSprites[selectedIconIndex];
        }
    }
}