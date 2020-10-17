using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFlashCamera : MonoBehaviour
{
    [SerializeField]
    private CameraFlash cameraFlash;

    private void Start()
    {
        cameraFlash.OnFlash += Flash;
    }

    private void Flash()
    {
        throw new System.NotImplementedException();
    }
}
