using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public IEnumerator ScreenShake(object[] parms)
    {
        // Parameter One = Shake Amount
        // Parameter Two = Shake Length

        int shakeLength = (int)parms[1];
        Vector3 position = new Vector3((float)parms[0], 0, 0);

        for (int i = 0; i < shakeLength; i++)
        {
            gameObject.transform.position += position;

            yield return new WaitForSeconds(0.05f);

            gameObject.transform.position -= position;

            yield return new WaitForSeconds(0.05f);
        }
    }
}
