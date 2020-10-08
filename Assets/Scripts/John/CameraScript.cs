using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public IEnumerator ScreenShake(float shakeAmount)
    {
        print("SHAKING WOW!");
        yield return new WaitForSeconds(1f);
    }
}
