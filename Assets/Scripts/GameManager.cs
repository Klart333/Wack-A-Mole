using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private GameObject loseScreen;
    [SerializeField]
    public AudioSource bakgrundsMusik;

    private Collider[] hitResults;


    private int lives = 1;
    internal int score;
    internal bool mullvadStillAlive;
    internal float poängtime = 5;

    internal AudioSource Slåljud;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        Slåljud = GetComponent<AudioSource>();
        loseScreen.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            IClickable target = null;

            Collider[] hitColliders = Physics.OverlapSphere(mousePos, 1);

            foreach (Collider hit in hitColliders)
            {
                target = hit.GetComponent<IClickable>();
                if (target != null)
                {
                    target.OnClicked();
                }

            }

            if (target == null)
            {
                lives--;

                if (lives <= 0)
                {
                    GameOver();
                }
            }
        }

        if (mullvadStillAlive)
        {
            poängtime -= Time.deltaTime;
        }
    }

    public void GameOver()
    {
        bakgrundsMusik.Stop();
        Slåljud.PlayOneShot(Slåljud.clip);

        Time.timeScale = 0;

        loseScreen.SetActive(true);
        print("Game Over");
    }
}
