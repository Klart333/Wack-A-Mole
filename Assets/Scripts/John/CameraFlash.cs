﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlash : MonoBehaviour, IClickable
{
    private SharkSpawner sharkSpawner;

    public event Action OnFlash = delegate { };

    public int cameraFlashes = 3;

    private void Awake()
    {
        OnFlash = delegate { };
        sharkSpawner = FindObjectOfType<SharkSpawner>();
    }

    public void OnClicked()
    {
        if (cameraFlashes > 0)
        {
            Flash();
        }
    }

    private void Flash()
    {
        Audio.Instance.PlaySoundEffect("", "CameraSound"); // Plays the flash sound, doesn't necessarily need a mixer group 

        Shark[] sharks = FindObjectsOfType<Shark>();

        foreach (Shark shark in sharks)
        {
            shark.gameObject.SetActive(false); // Sends every shark on the screen back to its pool
        }

        cameraFlashes--;
        OnFlash(); // Calls the event, so that every connected script does its part

        StartCoroutine("DeactivateSharkSpawnerForDelay", 3); 
    }

    private IEnumerator DeactivateSharkSpawnerForDelay(float delay) // Deactivates the sharkspawner for delay seconds
    {
        sharkSpawner.gameObject.SetActive(false);

        yield return new WaitForSeconds(delay);

        sharkSpawner.gameObject.SetActive(true);
    }
}
