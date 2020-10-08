using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIScoreScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private UIResponseText responseText;

    private int score;
    public int Score { get { return score; } } // Protects score so that only his clone big brother can be accesed, read only

    [SerializeField]
    private int baseValueForSharks = 12; // Try to get different values for different sharks

    private void Start()
    {
        GameManager.Instance.OnSharkKilled += IncreaseScore;
    }

    private void UpdateScore()
    {
        scoreText.text = Score.ToString();
    }

    private void IncreaseScore(float sharkTimeToKill)
    {
        int value = baseValueForSharks - (int)sharkTimeToKill;
        if (sharkTimeToKill < 0.5) // DOUBLE (Cool effect)
        {
            value *= 2;
            responseText.PrintDef("Double!");
            Camera.main.GetComponent<CameraScript>().StartCoroutine("ScreenShake", 10);
        }

        score += value;
        UpdateScore();
    }

}
