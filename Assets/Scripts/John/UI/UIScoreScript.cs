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
        int value = baseValueForSharks - Mathf.RoundToInt(sharkTimeToKill);
        value = Mathf.RoundToInt((float)value * (1 + GameManager.Instance.hitSpree / 10));

        if (sharkTimeToKill < 0.75f) // DOUBLE (Cool effect)
        {
            value *= 2;

            responseText.PrintDef("Double!");

            object[] parms = new object[] {0.1f, 2}; 
            Camera.main.GetComponent<CameraScript>().StartCoroutine("ScreenShake", parms); // Could add it to the event but calling it from here allows me to easier implement bigger screenshakes from gunshots and such
        }

        score += value;
        UpdateScore();
    }

}
