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

        if (CheckForGunHits(sharkTimeToKill, value) == true)
        {
            AddAndUpdateScore(value);
            return;
        }


        if (sharkTimeToKill < 0.75f) // DOUBLE (Cool effect)
        {
            value = MultiplyEffect(value, 2f, "Double!", 0.1f, 2);
        }

        AddAndUpdateScore(value);
    }

    private void AddAndUpdateScore(int value)
    {
        score += value;
        UpdateScore();
    }

    private bool CheckForGunHits(float sharkTimeToKill, int value)
    {
        if (PowerupManager.Instance.gunActive && sharkTimeToKill < 0.75f)
        {
            MultiplyEffect(value, 6f, "Sextuple!", 3, 0.4f);
            return true;
        }
        else if (PowerupManager.Instance.gunActive)
        {
            MultiplyEffect(value, 3f, "Triple!", 2f, 0.1f);
            return true;
        }

        return false;
    }

    private int MultiplyEffect(int value, float multiplicativeValue, string response, float screenShakeAmount, float screenShakeTimes)
    {
        value = Mathf.RoundToInt(value * multiplicativeValue);
        responseText.PrintDef(response);

        float[] parms = new float[] { screenShakeAmount, screenShakeTimes };

        Camera.main.GetComponent<CameraScript>().StartCoroutine("ScreenShake", parms); // Could add it to the event but calling it from here allows me to easier implement bigger screenshakes from gunshots and such
        
        return value;
    }
}
