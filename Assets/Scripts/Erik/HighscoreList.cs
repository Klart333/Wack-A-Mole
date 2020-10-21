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


    public void Addperson(string name, int score)
    {
        names.Add(name);
        scores.Add(score);
        BubbleSort(scores, names);
        while (names.Count > 9)
        {
            names.RemoveAt(names.Count - 1);
        }
        while (scores.Count > 9)
        {
            scores.RemoveAt(scores.Count - 1);
        }
    }

    public void Reset()
    {
        names.RemoveRange(0, names.Count);
        scores.RemoveRange(0, scores.Count);
        highname.text = "";
        highscore.text = "";
        Uppdate();
    }

    public void BubbleSort(List<int> scores, List<string> names)
    {
        int i, j;
        int N = scores.Count;

        for (j = N - 1; j > 0; j--)
        {
            for (i = 0; i < j; i++)
            {
                if (scores[i] < scores[i + 1])
                {
                    Exchange(scores, i + 1, i);
                    Exchange(names, i + 1, i);
                }
            }
        }
    }

    void Exchange(List<int> list, int first, int second)
    {
        int tempSlot = list[first];
        list[first] = list[second];
        list[second] = tempSlot;
    }
    void Exchange(List<string> list, int first, int second)
    {
        string tempSlot = list[first];
        list[first] = list[second];
        list[second] = tempSlot;
    }

    public void Uppdate()
    {
        BubbleSort(scores, names);
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
        Uppdate();
    }
}