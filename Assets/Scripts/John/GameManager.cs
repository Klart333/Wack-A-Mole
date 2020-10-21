using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GameManager : MonoBehaviour // STOR FACKING NOTE: Du borde verkligen poola alla hajar, SPECIELLT om det ska funka på mobil
{ 
    public static GameManager Instance;

    [SerializeField]
    private float difficultyIncreasedFromSharkKill = 0.1f; // Rather long than ununderstanble, right?

    [SerializeField]
    private float startDifficulty = 1;

    [SerializeField]
    public float doubleTime = 0.75f;

    private GameObject loseScreen;

    public int hitSpree = 0;
    public bool Gameover = false;
    public float DifficultyMultiplier { get; private set; }

    public event Action<float> OnSharkKilled;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        OnSharkKilled += IncreaseDifficultyOnSharkKill;
    }

    private void Start()
    {
        DifficultyMultiplier = startDifficulty;

        SceneManager.activeSceneChanged += ActiveSceneChanged; // PIECE OF SHIT DOESN'T EVEN WORK
    }

    private void ActiveSceneChanged(Scene currentScene, Scene nextScene)
    {
        if (nextScene.buildIndex == 1) // Resett
        {
            hitSpree = 0;
            DifficultyMultiplier = startDifficulty;
            Gameover = false;

            Pool.dictionaryPools = new Dictionary<PooledMonoBehaviour, Pool>(); // Removes the Pools

            OnSharkKilled = delegate { }; // Resets the event
            OnSharkKilled += IncreaseDifficultyOnSharkKill; // Adds this back


        }
    }

    public void GameOver()
    {
        StartCoroutine("SwitchSceneAfterDelay", 0.5f);
    }

    public void SharkKilled(float timeToKill)
    {
        OnSharkKilled?.Invoke(timeToKill); // Checks that the event isn't null
    }

    private void IncreaseDifficultyOnSharkKill(float timer)
    {
        DifficultyMultiplier += difficultyIncreasedFromSharkKill;
    }

    private IEnumerator SwitchSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        print("Switching Scene");
        SceneManager.LoadScene(2);
    }

}
