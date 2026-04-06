using UnityEngine;
using UnityEngine.UI;

public class IconButton : MonoBehaviour
{
    public int iconIndex;           // Index của icon này
    public Image iconImage;         // Image hiển thị sprite
    public GameObject selectedFrame; // Viền khi được chọn (dashed border như ảnh 1)
    
    private SelectIconManager manager;
    
    public void Setup(int index, Sprite sprite, SelectIconManager selectIconManager)
    {
        iconIndex = index;
        
        // Tự động tìm Image component nếu chưa gán
        if (iconImage == null)
            iconImage = GetComponent<Image>();
        
        if (iconImage != null && sprite != null)
        {
            iconImage.sprite = sprite;
            iconImage.color = Color.white; // Reset color về trắng (đầy đủ alpha)
            iconImage.SetAllDirty(); // Bắt buộc Unity rebuild UI
            Debug.Log($"IconButton.Setup: Assigned sprite '{sprite.name}' to iconImage");
        }
        
        manager = selectIconManager;
        
        UpdateSelectedState();
    }
    
    void Start()
    {
        if (manager == null)
            manager = FindObjectOfType<SelectIconManager>();
        
        // Hiển thị viền nếu đây là icon đang chọn
        UpdateSelectedState();
    }
    
    public void OnClick()
    {
        manager.SelectIcon(iconIndex);
    }
    
    public void UpdateSelectedState()
    {
        if (selectedFrame != null)
        {
            bool isSelected = (IconManager.Instance.selectedIconIndex == iconIndex);
            selectedFrame.SetActive(isSelected);
        }
    }
}