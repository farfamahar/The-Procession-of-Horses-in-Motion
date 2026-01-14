using UnityEngine;

public class FrameLimiter : MonoBehaviour
{
    void Start()
    {
        QualitySettings.vSyncCount = 2;

        Application.targetFrameRate = 30;
    }
}
