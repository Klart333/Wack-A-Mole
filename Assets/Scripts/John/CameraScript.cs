using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public IEnumerator ScreenShake(float[] parms)
    {
        // Parameter One = Shake Amount
        // Parameter Two = Shake Times

        Vector3 position = new Vector3((float)parms[0], 0, 0);

        int shakeLength = Mathf.RoundToInt(parms[1]);

        for (int i = 0; i < shakeLength; i++)
        {
            gameObject.transform.position += position;

            yield return new WaitForSeconds(0.05f);

            gameObject.transform.position -= position;

            yield return new WaitForSeconds(0.05f);
        }
    }
}
