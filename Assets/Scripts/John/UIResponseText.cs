using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIResponseText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI responseText;

    [SerializeField]
    private Animator responseAnimator;

    public IEnumerator Print(string text, float time)
    {
        responseText.text = text;
        responseAnimator.SetTrigger("Juice");

        yield return new WaitForSeconds(time);

        responseText.text = "";
    }
    public IEnumerator PrintDef(string text) // Default Time
    {
        responseText.text = text;
        responseAnimator.SetTrigger("Juice");

        yield return new WaitForSeconds(1f);

        responseText.text = "";
    }
}
