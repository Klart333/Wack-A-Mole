using UnityEngine;
using TMPro;
public class SaveScoreAndName : MonoBehaviour
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
