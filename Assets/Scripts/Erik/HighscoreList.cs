using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreList : MonoBehaviour
{
    public Text highname;
    public Text highscore;
    public Text highnum;

    List<string> names = new List<string>();
    List<int> scores = new List<int>();

    public void Addperson(string name, int score)
    {
        names.Add(name);
        scores.Add(score);
    }

    public void Uppdate()
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

    private void Start()
    {
        Addperson("aaa", 900);
        Uppdate();
    }
}