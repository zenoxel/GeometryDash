using UnityEngine;
using UnityEngine.UI;

public class IconButton : MonoBehaviour
{
    public int iconIndex;           // Index của icon này
    public Image iconImage;         // Image hiển thị sprite
    public GameObject selectedFrame; // Viền khi được chọn (dashed border như ảnh 1)
    
    private SelectIconManager manager;
    
    void Start()
    {
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