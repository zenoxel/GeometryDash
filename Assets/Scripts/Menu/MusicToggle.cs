using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite musicOnNormal;
    public Sprite musicOnPressed;
    public Sprite musicOffNormal;
    public Sprite musicOffPressed;

    private bool _isMuted = false;
    private Button _btn;
    private Image _img;

    void Awake()
    {
        _btn = GetComponent<Button>();
        _img = GetComponent<Image>();
        _btn.onClick.AddListener(Toggle);
    }

    void Toggle()
    {
        _isMuted = !_isMuted;
        AudioListener.volume = _isMuted ? 0f : 1f;
        UpdateSprites();
        PlayerPrefs.SetInt("MusicMuted", _isMuted ? 1 : 0);
    }

    void UpdateSprites()
    {
        SpriteState ss = new SpriteState();
        if (_isMuted)
        {
            _img.sprite = musicOffNormal;
            ss.highlightedSprite = musicOffPressed;
            ss.pressedSprite     = musicOffPressed;
        }
        else
        {
            _img.sprite = musicOnNormal;
            ss.highlightedSprite = musicOnPressed;
            ss.pressedSprite     = musicOnPressed;
        }
        _btn.spriteState = ss;
    }

    void Start()
    {
        _isMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        AudioListener.volume = _isMuted ? 0f : 1f;
        UpdateSprites();
    }
}