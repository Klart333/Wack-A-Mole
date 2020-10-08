using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private GameObject loseScreen;

    [SerializeField]
    private AudioSource bakgrundsMusik;

    [SerializeField]
    private float maxDifficulty = 3; // Easily changeable

    [SerializeField]
    private float difficultyIncreasedFromSharkKill = 0.02f; // Rather long than ununderstanble, right?

    [SerializeField]
    private float startDifficulty = 1;

    public float difficultyMultiplier { get; private set; }


    public event Action<float> OnSharkKilled;

    private int lives = 1;

    public AudioSource Slåljud;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        difficultyMultiplier = startDifficulty;

        Slåljud = GetComponent<AudioSource>();
        loseScreen.SetActive(false);

        OnSharkKilled += IncreaseDifficultyOnSharkKill; 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = GetWorldPointClicked();

            IClickable target = null;
            target = CheckForMouseHit(position, target);

            if (target == null)
            {
                lives--;

                if (lives <= 0)
                {
                    GameOver();
                }
            }
        }

    }

    private IClickable CheckForMouseHit(Vector3 position, IClickable target)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, 0.1f);

        foreach (Collider hit in hitColliders)
        {
            target = hit.GetComponent<IClickable>();
            if (target != null)
            {
                target.OnClicked();
            }
        }

        return target;
    }

    private Vector3 GetWorldPointClicked()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f); // I don't understand why z has to be 10, and it has to be 10
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        return mousePos;
    }

    public void GameOver()
    {
        bakgrundsMusik.Stop();
        Slåljud.PlayOneShot(Slåljud.clip);

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
        if (difficultyMultiplier <= maxDifficulty)
        {
            difficultyMultiplier += difficultyIncreasedFromSharkKill;
        }
        
    }

}
