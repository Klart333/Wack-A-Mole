using UnityEngine;
using TMPro;
public class SaveScoreAndName : MonoBehaviour // On the button pressed we save the game with the values from the text and namefield
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TMP_InputField nameField;

    public void SaveValues()
    {
        Saveclass.Instance.SaveGame(int.Parse(scoreText.text), nameField.text);
    }
}
