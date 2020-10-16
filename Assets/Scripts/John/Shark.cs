using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class Shark : PooledMonoBehaviour, IClickable // I realise in hindsight that i probably could have used a blend tree for the animation
{
    [SerializeField]
    private float totalTimeBeforeChomp = 8; // The time is divided over the amount of sharkPhases

    [SerializeField]
    private int sharkBitePhases = 5;
    
    [SerializeField]
    private float moveSpeed = 5;

    [SerializeField]
    private float sharkGrowSpeed = 0.5f;

    private Animator animator;

    private float killTimer;
    private bool inPosition;

    private bool[] sharkTurning;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sharkTurning = new bool[3];
    }
    private void Update()
    {
        killTimer += Time.deltaTime;

        if (GameManager.Instance.GameOvering)
        {
            StopCoroutine("MoveToX");
        }

        if (inPosition)
        {
            Grow();
        }
    }
    private void Grow()
    {
        if (GameManager.Instance.GameOvering) // The sharks stop to give the illusion of the timeScale being set to 0
            return;


        transform.localScale += new Vector3(1, 1, 0) * Time.deltaTime * sharkGrowSpeed;
    }
    public void SetRoamGoal()
    {
        float goal = GetRandomXPos();

        StartCoroutine("MoveToX", goal);
    }

    private IEnumerator MoveToX(float xGoal)
    {
        bool goingLeft = true;

        if (transform.position.x < 0)
        {
            goingLeft = false;
        }

        if (goingLeft)
        {
            while (transform.position.x > xGoal)
            {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                yield return new WaitForSeconds(0.001f);
                TurnAtShortDistance(xGoal);
            }
            inPosition = true;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            while (transform.position.x < xGoal)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                yield return new WaitForSeconds(0.001f);
                TurnAtShortDistance(xGoal);
            }
            inPosition = true;
        }

        StartCoroutine("SharkBitePhases"); // When we are in position we start the SharkBitePhases

    }

    private void TurnAtShortDistance(float xGoal)
    {
        if (Mathf.Abs(transform.position.x - xGoal) <= 2 && !sharkTurning[0])
        {
            sharkTurning[0] = true;
            animator.SetTrigger("SharkTurn1");
        }

        if (Mathf.Abs(transform.position.x - xGoal) <= 1 && !sharkTurning[1])
        {
            sharkTurning[1] = true;
            animator.SetTrigger("SharkTurn2");
        }

        if (Mathf.Abs(transform.position.x - xGoal) <= 0.5f && !sharkTurning[2])
        {
            sharkTurning[2] = true;
            animator.SetTrigger("SharkTurn3");
        }
    }


    private float GetRandomXPos()
    {
        float screenXPos = UnityEngine.Random.Range(100, 1800);
        Vector3 randomPos = Camera.main.ScreenToWorldPoint(new Vector3(screenXPos, 0, 0));
        return randomPos.x;
    }

    public void OnClicked()
    {
        GameManager.Instance.SharkKilled(killTimer);

        ReturnToPool(); // Returns the shark to the pool
    }

    private IEnumerator SharkBitePhases()
    {
        float timeBetweenPhases = totalTimeBeforeChomp / sharkBitePhases;

        for (int i = 1; i <= sharkBitePhases; i++)
        {
            if (GameManager.Instance.GameOvering)
                break;

            animator.SetTrigger("SharkBite" + i.ToString());

            yield return new WaitForSeconds(timeBetweenPhases);
            if (i == sharkBitePhases)
            {
                StartCoroutine("Bite");
            }
        }
    }

    private IEnumerator Bite()
    {
        animator.SetTrigger("Gameover"); 
        GameManager.Instance.GameOvering = true;

        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.GameOver();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        ResetShark();
    }
    private void ResetShark()
    {
        StopCoroutine("SharkBitePhases");
        animator.SetTrigger("SharkRoam");

        gameObject.transform.localScale = Vector3.one;

        killTimer = 0;

        inPosition = false;
        for (int i = 0; i < sharkTurning.Length; i++)
        {
            sharkTurning[i] = false;
        }
    }

}
