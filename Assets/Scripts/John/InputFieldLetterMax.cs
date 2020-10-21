using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InputFieldLetterMax : MonoBehaviour
{
    [SerializeField]
    private int letterMax = 3;

    private TMP_InputField inputField;
    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
    }
    private void Update()
    {
        if (inputField.text.Length > letterMax)
        {
            string allowedText = "";

            for (int i = 0; i < letterMax; i++)
            {
                allowedText += inputField.text[i];
            }
            inputField.text = allowedText;
        }
    }

}
