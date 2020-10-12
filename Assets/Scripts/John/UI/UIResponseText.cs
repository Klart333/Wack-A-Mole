using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIResponseText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI responseText;

    bool increasingSize = false; // For effect we want the increasing to keep increasing
    public IEnumerator Print(string text, float time)
    {
        responseText.text = text;

        object[] parms = new object[2] { 0.5f, text };

        StartCoroutine("GrowTextThenShrink", parms);
        yield return new WaitForSeconds(time / 2);
        StartCoroutine("ShrinkText", parms);
        yield return new WaitForSeconds(time / 2);


        ResetText();
    }

    public void PrintDef(string text) // Default Time
    {
        responseText.text = text;

        object[] parms = new object[2] { 0.5f, text };

        StartCoroutine("GrowTextThenShrink", parms);
    }

    private IEnumerator GrowTextThenShrink(object[] parms)
    {
        float maxFontIncrease = 70;
        for (int i = 0; i < maxFontIncrease; i++)
        {
            increasingSize = true;

            responseText.text = (string)parms[1];
            if (responseText.fontSize < 160)
            {
                responseText.fontSize = responseText.fontSize + 1;
            }
            yield return new WaitForSeconds((float)parms[0] / maxFontIncrease);
        }
        increasingSize = false;
        StartCoroutine("ShrinkText", parms);
    }

    private IEnumerator ShrinkText(object[] parms)
    {
        float maxFontIncrease = 70;
        for (int i = 0; i < maxFontIncrease; i++)
        {
            if (increasingSize == false)
            {
                responseText.text = (string)parms[1];
                if (responseText.fontSize > 80)
                {
                    responseText.fontSize -= 2;
                }

                yield return new WaitForSeconds((float)parms[0] / maxFontIncrease);
            }

        }
        ResetText();
    }


    private void ResetText()
    {
        if (increasingSize == false)
        {
            responseText.fontSize = 80; // On the last call we can reset
        }
        responseText.text = "";
    }

}
