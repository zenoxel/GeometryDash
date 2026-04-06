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
        // Xóa buttons cũ nếu có
        foreach (Transform child in iconGrid)
            Destroy(child.gameObject);

        allButtons = new IconButton[iconSprites.Length];

        for (int i = 0; i < iconSprites.Length; i++)
        {
            GameObject obj = Instantiate(iconButtonPrefab, iconGrid);
            IconButton btn = obj.GetComponent<IconButton>();
            btn.Setup(i, iconSprites[i], SelectIcon);
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
        SceneManager.LoadScene("MainMenu");
    }
}