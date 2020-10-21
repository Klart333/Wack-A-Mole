using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
public class Saveclass : MonoBehaviour
{
    [Serializable]
    public class Save
    {
        public List<string> names = new List<string>();
        public List<int> scores = new List<int>();
    }

    public static Saveclass Instance;

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

        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene currentScene, Scene nextScene)
    {
        if (nextScene.buildIndex == 0)
        {
            LoadGame(); // We show the scores when we load into the menu
            HighscoreList.Instance.ShowScores();
        }
    }

    public void SaveGame(int newScore, string newName) // Don't want to override the save file evertime we save, we want to add to it, specifically the score we just got
    {
        BinaryFormatter bf = new BinaryFormatter();

        if (File.Exists(Application.persistentDataPath + "/SaveData.Highscores"))
        {
            FileStream openedFile = File.Open(Application.persistentDataPath + "/SaveData.Highscores", FileMode.Open);
            Save save = (Save)bf.Deserialize(openedFile);
            openedFile.Close();

            foreach  (int savedScore in save.scores)
            {
                if (newScore > savedScore)
                {
                    int index = save.scores.IndexOf(savedScore);

                    save.scores[index] = newScore;
                    save.names[index] = newName;
                    break; // We want to break out of the loop if we replaced an item for two reasons, 1. We're done 2. We just modified the collection and need to quickly flee the scene 
                }
            }
            // We make a new file and put the saved values from the last in there
            FileStream createdFile = File.Create(Application.persistentDataPath + "/SaveData.Highscores");

            bf.Serialize(createdFile, save);
            createdFile.Close();
            print("Game is saved. We saved this many scores, names: " + save.scores.Count + ", " + save.names.Count);
        }
        else // The only time there wont exist a file is the first time, So we want to create a file with 8 empty spots plus the new name and score
        {
            print("There was no File");

            Save save = new Save();
            FileStream createdFile = File.Create(Application.persistentDataPath + "/SaveData.Highscores");

            save.scores.Add(newScore);
            save.names.Add(newName);

            for (int i = 0; i < 8; i++)
            {
                save.scores.Add(0);
                save.names.Add("");
            }
            
            bf.Serialize(createdFile, save);
            createdFile.Close();
            print("Game is saved. We saved this many scores, names: " + save.scores.Count + ", " + save.names.Count);
        }
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData.Highscores"))
        {
            HighscoreList.Instance.Reset();
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/SaveData.Highscores", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            print("Loading this many scores " + save.scores.Count);
            foreach (var score in save.scores)
            {
                HighscoreList.Instance.scores.Add(score);
            }

            print("Loading this many names " + save.scores.Count);
            foreach (var name in save.names)
            {
                HighscoreList.Instance.names.Add(name);
            }

            print("Game data loaded!");
        }
        else
        {
            print("There is no save data!");
        }
    }


}
