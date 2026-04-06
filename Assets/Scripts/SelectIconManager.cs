using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectIconManager : MonoBehaviour
{
    [Header("Sprites - kéo đúng 16 sprite vào đây")]
    public Sprite[] iconSprites;

    [Header("UI References")]
    public Transform iconGrid;        // Object IconGrid
    public GameObject iconButtonPrefab;
    public Image previewImage;        // Icon to hiển thị trên cùng

    private IconButton[] allButtons;

    void Start()
    {
        if (IconManager.Instance == null)
        {
            GameObject go = new GameObject("IconManager");
            go.AddComponent<IconManager>();
        }

        GenerateButtons();
        UpdatePreview();
    }

    void GenerateButtons()
    {
        if (iconGrid == null || iconButtonPrefab == null)
        {
            Debug.LogError("IconGrid or iconButtonPrefab not assigned!");
            return;
        }

        Debug.Log($"GenerateButtons: iconSprites.Length = {iconSprites.Length}");
        for (int i = 0; i < iconSprites.Length; i++)
        {
            Debug.Log($"  [{i}] = {(iconSprites[i] != null ? iconSprites[i].name : "NULL")}");
        }

        // Xóa buttons cũ nếu có
        foreach (Transform child in iconGrid)
            Destroy(child.gameObject);

        allButtons = new IconButton[iconSprites.Length];

        for (int i = 0; i < iconSprites.Length; i++)
        {
            GameObject obj = Instantiate(iconButtonPrefab, iconGrid);
            IconButton btn = obj.GetComponent<IconButton>();
            
            // Verify sprite before passing
            if (iconSprites[i] == null)
            {
                Debug.LogError($"GenerateButtons: iconSprites[{i}] is NULL!");
            }
            
            btn.Setup(i, iconSprites[i], this);
            allButtons[i] = btn;
        }
    }

    public void SelectIcon(int index)
    {
        IconManager.Instance.SelectIcon(index);

        foreach (IconButton btn in allButtons)
            btn.UpdateSelectedState();

        UpdatePreview();
    }

    void UpdatePreview()
    {
        if (previewImage != null && iconSprites.Length > 0)
            previewImage.sprite = 
                iconSprites[IconManager.Instance.selectedIconIndex];
    }

    public void OnBackButton()
    {
        Debug.Log("OnBackButton clicked!");
        // Load scene by index (0 = MainMenu)
        SceneManager.LoadScene(0);
    }
}