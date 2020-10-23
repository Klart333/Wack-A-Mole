using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GameManager : MonoBehaviour // The GameManager ties all the seperate scripts together, thus there is only one (well not all but most)
{ 
    public static GameManager Instance;

    // The big benefit of SerializeFields are the flexibilty of changing values in the inspector, especially when you have multiple of the same script and still want variability (This is as singleton but still)
    [SerializeField]
    private float difficultyIncreasedFromSharkKill = 0.1f; // Rather long than ununderstanble, right?

    [SerializeField]
    private float startDifficulty = 1;

    [SerializeField]
    public float doubleTime = 0.75f;

    public event Action<float> OnSharkKilled = delegate { }; // Initialises the event into a empty delegate so that it doesn't blow up if it's empty

    public int hitSpree = 0;
    public bool Gameover = false;
    public float DifficultyMultiplier { get; private set; }

    private int localScore; // Only used for passing into the SetScore, fake hence called local

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else // Don't want two, a problem when we return to the gamescene and there is a GameManager there
        {
            Destroy(gameObject);
        }

        OnSharkKilled += IncreaseDifficultyOnSharkKill;
    }

    private void Start()
    {
        DifficultyMultiplier = startDifficulty;

        SceneManager.activeSceneChanged += ActiveSceneChanged; // The ActiveSceneChanged method wont call when we enter the gamescene for the first time because the event needs be registered
    }

    private void ActiveSceneChanged(Scene currentScene, Scene nextScene)
    {
        if (nextScene.buildIndex == 1) // When we enter the gamescene we want to reset
        {
            hitSpree = 0;
            DifficultyMultiplier = startDifficulty;
            Gameover = false;

            Pool.dictionaryPools = new Dictionary<PooledMonoBehaviour, Pool>(); // Removes the stored Pools


            OnSharkKilled = delegate { }; // Resets the event, so that it doesn't have old functions with messed up references 
            
            OnSharkKilled += Audio.Instance.AudioOnSharkKilled; // Adds these back
            OnSharkKilled += IncreaseDifficultyOnSharkKill; 
        }

        if (nextScene.buildIndex == 2) // If the scene is the gameover scene we need to pass in the score
        {
            SetGameoverScore(localScore);
        }
    }

    public void SharkKilled(float timeToKill)
    {
        OnSharkKilled(timeToKill);
    }

    private void IncreaseDifficultyOnSharkKill(float timer)
    {
        DifficultyMultiplier += difficultyIncreasedFromSharkKill;
    }

    public void GameOver()
    {
        localScore = FindObjectOfType<UIScoreScript>().Score;
        StartCoroutine("SwitchSceneAfterDelay", 0.5f);
    }

    private IEnumerator SwitchSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        print("Switching Scene");
        SceneManager.LoadScene(2);
    }

    private void SetGameoverScore(int score)
    {
        FindObjectOfType<ScorePanele>().SkrivPoeng(score);
    }

}
