using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        loseScreen = GameObject.Find("LosePanel");
        DifficultyMultiplier = startDifficulty;

        SceneManager.activeSceneChanged += ActiveSceneChanged; // PIECE OF SHIT DOESN'T EVEN WORK
    }

    private void ActiveSceneChanged(Scene currentScene, Scene nextScene)
    {
        if (nextScene.buildIndex == 1) // Resett
        {
            loseScreen = GameObject.Find("LosePanel");
            DifficultyMultiplier = startDifficulty;
            Gameover = false;
        }
    }

    public void GameOver()
    {
        loseScreen.SetActive(true);

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
