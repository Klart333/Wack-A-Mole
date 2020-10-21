using System;
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

        Audio.Instance.PlaySoundEffect("", "CameraSound");

        Shark[] sharks = FindObjectsOfType<Shark>();

        foreach (Shark shark in sharks)
        {
            shark.gameObject.SetActive(false);
        }

        cameraFlashes--;
        OnFlash();

        StartCoroutine("DeactivateSharkSpawner");
    }

    private IEnumerator DeactivateSharkSpawner()
    {
        sharkSpawner.gameObject.SetActive(false);

        yield return new WaitForSeconds(3);

        sharkSpawner.gameObject.SetActive(true);
    }
}
