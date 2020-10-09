using System;
using UnityEngine;

public class GameManager : MonoBehaviour // STOR FACKING NOTE: Du borde verkligen poola alla hajar, SPECIELLT om det ska funka på mobil
{ 
    public static GameManager Instance;

    [SerializeField]
    private GameObject loseScreen;

    [SerializeField]
    private AudioSource bakgrundsMusik;

    [SerializeField]
    private float maxDifficulty = 3; // Easily changeable

    [SerializeField]
    private float difficultyIncreasedFromSharkKill = 0.1f; // Rather long than ununderstanble, right?

    [SerializeField]
    private float startDifficulty = 1;
    public float difficultyMultiplier { get; private set; }

    public event Action<float> OnSharkKilled;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        difficultyMultiplier = startDifficulty;

        loseScreen.SetActive(false);

        OnSharkKilled += IncreaseDifficultyOnSharkKill;
    }

 

    public void GameOver()
    {
        bakgrundsMusik.Stop();

        Time.timeScale = 0;

        loseScreen.SetActive(true);
        print("Game Over");
    }

    public void SharkKilled(float timeToKill)
    {
        OnSharkKilled?.Invoke(timeToKill); // Checks that the event isn't null
    }

    private void IncreaseDifficultyOnSharkKill(float timer)
    {
        if (maxDifficulty != 0)
        {
            if (difficultyMultiplier <= maxDifficulty)
            {
                difficultyMultiplier += difficultyIncreasedFromSharkKill;
            }
        }
        else
        {
            difficultyMultiplier += difficultyIncreasedFromSharkKill;
        }
        
        
    }

}
