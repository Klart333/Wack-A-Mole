﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class Shark : PooledMonoBehaviour, IClickable
{
    [SerializeField]
    private float totalTimeBeforeChomp = 6; // The time is divided over the amount of sharkPhases

    [SerializeField]
    private int phases = 3;

    private Animator animator;

    private float killTimer;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        ResetShark();

        StartCoroutine("Phases");

    }

    private void Update()
    {
        killTimer += Time.deltaTime;

        Grow();
    }

    private void ResetShark()
    {
        gameObject.transform.localScale = Vector3.one;
        killTimer = 0;
    }

    private void Grow()
    {
        if (GameManager.Instance.GameOvering) // The sharks stop to give the illusion of the timeScale being set to 0
            return;


        transform.localScale += new Vector3(1, 1, 0) * Time.deltaTime;
    }

    public void OnClicked()
    {
        GameManager.Instance.SharkKilled(killTimer);

        ReturnToPool(); // Returns the shark to the pool
    }

    private IEnumerator Phases()
    {
        float timeBetweenPhases = totalTimeBeforeChomp / phases;

        for (int i = 1; i <= phases; i++)
        {
            if (GameManager.Instance.GameOvering)
                break;

            animator.ResetTrigger("Phase1"); // When called from the start the phase1 is not used and then stays activated

            animator.SetTrigger("Phase" + i.ToString());

            yield return new WaitForSeconds(timeBetweenPhases);
            if (i == phases)
            {
                StartCoroutine("Bite");
            }
        }
    }

    private IEnumerator Bite()
    {
        animator.SetTrigger("SharkBite");
        GameManager.Instance.GameOvering = true;

        yield return new WaitForSeconds(0.55f);

        GameManager.Instance.GameOver();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        StopCoroutine("Phases");
    }

 
}
