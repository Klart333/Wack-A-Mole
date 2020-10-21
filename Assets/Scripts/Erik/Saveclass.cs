using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Saveclass : MonoBehaviour
{
    [Serializable]
    public class Save
    {
        public List<string> names = new List<string>();
        public List<int> scores = new List<int>();
    }

    [SerializeField]
    private HighscoreList highscoreList;

    private void Awake()
    {
        highscoreList.scores.Add(12);
        highscoreList.names.Add("ABC");
        highscoreList.scores.Add(15);
        highscoreList.names.Add("BCA");
        highscoreList.scores.Add(120);
        highscoreList.names.Add("CBA");
        SaveGame();        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("space");
            LoadGame();
            highscoreList.Uppdate();
        }
    }

        public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();

        Save save = new Save();
        FileStream createdFile = File.Create(Application.persistentDataPath + "/SaveData.Mauritz");

        save.scores.AddRange(highscoreList.scores);
        save.names.AddRange(highscoreList.names);
        bf.Serialize(createdFile, save);
        createdFile.Close();
        print("Game is saved. We saved this many scores, names: " + save.scores.Count + ", " + save.names.Count);
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData.Mauritz"))
        {
            highscoreList.Reset();
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/SaveData.Mauritz", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            print("Loading this many scores " + save.scores.Count);
            foreach (var score in save.scores)
            {
                highscoreList.scores.Add(score);
            }
            print("Loading this many names " + save.scores.Count);
            foreach (var name in save.names)
            {
                highscoreList.names.Add(name);
            }

            print("Game data loaded!");
        }
        else
        {
            print("There is no save data!");
        }
    }
}


