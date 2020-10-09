using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour, IClickable
{
    [SerializeField]
    private float totalTimeBeforeChomp = 6; // The time is divided over the amount of sharkPhases

    [SerializeField]
    private int phases = 3;

    private Animator animator;

 

    private float killTimer;
    
    private void Start()
    {
        animator = GetComponent<Animator>();

        StartCoroutine("Phases");
    }

    private void Update()
    {
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

    private IEnumerator Phases()
    {
        float timeBetweenPhases = totalTimeBeforeChomp / phases;

        for (int i = 1; i <= phases; i++)
        {
            animator.SetTrigger("Phase" + i.ToString());

            yield return new WaitForSeconds(timeBetweenPhases);

            if (i == phases)
            {
                Bite();
            }
        }
    }

    private void Bite()
    {
        animator.SetTrigger("SharkBite");
        // Gameover
    }
}
