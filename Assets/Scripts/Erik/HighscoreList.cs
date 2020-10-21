using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreList : MonoBehaviour
{
    public Text highname;
    public Text highscore;
    public Text highnum;

    [HideInInspector]
    public List<string> names = new List<string>();
    [HideInInspector]
    public List<int> scores = new List<int>();

    private void Start()
    {
        ShowScores();
    }

    public void Addperson(string name, int score)
    {
        names.Add(name);
        scores.Add(score);
    }

    public void Reset()
    {
        names.RemoveRange(0, names.Count);
        scores.RemoveRange(0, scores.Count);
        highname.text = "";
        highscore.text = "";

        ShowScores();
    }

    public void ShowScores()
    {
        foreach (string name in names)
        {
            highname.text += name + "\n";
        }

        foreach (int score in scores)
        {
            highscore.text += score + "\n";
        }
    }
}