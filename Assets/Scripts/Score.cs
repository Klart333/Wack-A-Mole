using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    private Text myText;

    private void Start()
    {
        myText = GetComponent<Text>();
    }
    private void Update()
    {
        myText.text = Spel.Instance.score.ToString();
    }
}
