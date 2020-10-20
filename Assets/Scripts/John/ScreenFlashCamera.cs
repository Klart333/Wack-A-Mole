using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlashCamera : MonoBehaviour
{
    [SerializeField]
    private CameraFlash cameraFlash;

    [SerializeField]
    private Image flashImage;

    private void Start()
    {
        cameraFlash.OnFlash += Flash;
    }

    private void Flash()
    {
        //LERP!

        StartCoroutine("FlashThePanel");
    }

    private IEnumerator FlashThePanel()
    {
        float alphaGoal = 1;
        float time = 0.2f;
        float alpha = flashImage.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time) // Makes the alpha 1 
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, alphaGoal, t));
            flashImage.color = newColor;
            yield return null;
        }
        
        alpha = flashImage.color.a;
        alphaGoal = 0f;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time) // Makes the alpha 0
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, alphaGoal, t));
            flashImage.color = newColor;
            yield return null;
        }
    }

}
