using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MobileDetector : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern bool IsMobile();

    public bool IsRunningOnMobile()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return IsMobile();
#else
        return Application.platform == RuntimePlatform.Android;
#endif
    }
}
