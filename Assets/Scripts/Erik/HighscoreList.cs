using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreList : MonoBehaviour
{
    public static HighscoreList Instance; 

    public Text highname;
    public Text highscore;
    public Text highnum;

    [HideInInspector]
    public List<string> names = new List<string>();
    [HideInInspector]
    public List<int> scores = new List<int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else // Don't want two
        {
            Destroy(gameObject);
        }
    }

    public void Addperson(string name, int score)
    {
        names.Add(name);
        scores.Add(score);
    }
    
    public void Reset() // Removes all scores and updates the board to show nothing
    {
        names.RemoveRange(0, names.Count);
        scores.RemoveRange(0, scores.Count);
        highname.text = "";
        highscore.text = "";

        ShowScores();
    }

    public void ShowScores()
    {
        SortLists(); // Sorts the lists before dislaying them

        for (int i = 1; i <= names.Count; i++) // Needs to start at one and end equal to the count because of zero indexing
        {
            highname.text += names[names.Count - i] + "\n"; // Goes through the list backwards because of how the list is sorted
        }

        for (int i = 1; i <= scores.Count; i++) // Same here
        {
            highscore.text += scores[scores.Count - i] + "\n"; // Goes through the list backwards because of how the list is sorted
        }
    }

    public void SortLists()
    {
        List<int> unSortedScores = new List<int>();
        foreach (int score in scores)
        {
            unSortedScores.Add(score); // Can't assign the unSortedScores to scores becuase then when we sort scores the unSortedScores list is also sorted, for some reason
        }

        string[] sortedNames = new string[names.Count]; // An Array that can hold as many values as the names list has 
        scores.Sort();

        foreach (string name in names) // We want to match every name to every score
        {
            foreach (int oldScore in unSortedScores)
            {
                if (names.IndexOf(name) == unSortedScores.IndexOf(oldScore)) // If they previously were paired
                {
                    int index = scores.IndexOf(oldScore);
                    
                    sortedNames[index] = name; // Then we find the new index of the old score and assign it to the sorted string list
                    unSortedScores[unSortedScores.IndexOf(oldScore)] = 0; // We remove the score so that the same score cannot get multiple names, we cannot delete it becuase that would fuck up the index
                    break; // We don't have to check the rest, also gotta skeedadle from errors
                }
            }
            
        }

        names = sortedNames.ToList();
    }
}