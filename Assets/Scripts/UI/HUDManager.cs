using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TMP_Text txtAttempt;

    public void UpdateAttempt(int count)
    {
        if (txtAttempt != null)
            txtAttempt.text = $"ATTEMPT {count}";
    }

    public void UpdateProgress(float percent)
    {
        // Optional: hiện % trên HUD
    }

    // public void OnPauseClick()
    // {
    //     GameManager.Instance?.pauseManager?.OnPauseClick();
    // }
}