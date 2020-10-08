using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour, IClickable
{
    // A lot of SerializeField, this is for variability between sharks set in the inspector, easy :D

    [SerializeField]
    private Sprite[] sharkPhases;

    [SerializeField]
    private float totalTimeBeforeChomp = 12; // The time is divided over the amount of sharkPhases

    private float phaseTimer; // Could have just one 'timer' but it feels a lot clearer this way
    private float killTimer;
    
    private int currentPhase = 0;
    private void Update()
    {
        phaseTimer += Time.deltaTime;
        killTimer += Time.deltaTime;

        Grow();
    }

    private void Grow()
    {
        transform.localScale += new Vector3(1, 1, 0) * Time.deltaTime;
    }

    public void OnClicked()
    {
        GameManager.Instance.SharkKilled(killTimer);

        Destroy(gameObject);
    }


}
