using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    private Text myText;

    private void Awake()
    {
        myText = GetComponent<Text>();
    }
    private void Update()
    {
        myText.text = Spel.Instance.score.ToString();
    }
}
