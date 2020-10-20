using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScorePanele : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    int score;
    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }
    public void skrivPoeng()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = score;
    }

}
