﻿using System;
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


    public int hitSpree = 0;
    public bool GameOvering = false;
    public float DifficultyMultiplier { get; private set; }

    public event Action<float> OnSharkKilled;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        DifficultyMultiplier = startDifficulty;

        loseScreen.SetActive(false);

        OnSharkKilled += IncreaseDifficultyOnSharkKill;
    }

 

    public void GameOver()
    {
        bakgrundsMusik.Stop();

        Time.timeScale = 0;

        loseScreen.SetActive(true);
    }

    public void SharkKilled(float timeToKill)
    {
        OnSharkKilled?.Invoke(timeToKill); // Checks that the event isn't null
    }

    private void IncreaseDifficultyOnSharkKill(float timer)
    {
        if (maxDifficulty != 0)
        {
            if (DifficultyMultiplier <= maxDifficulty)
            {
                DifficultyMultiplier += difficultyIncreasedFromSharkKill;
            }
        }
        else
        {
            DifficultyMultiplier += difficultyIncreasedFromSharkKill;
        }
        
        
    }

}
