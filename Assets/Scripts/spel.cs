using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spel : MonoBehaviour
{
    public GameObject[] mulls;
    public Transform[] spawns;
    public bool[] full;
    public float timer;
    public GameObject nymull;
    public float range;
    public Camera camera;
    public int score;
    public int liv;
    public float x;
    public GameObject loseScreen;
    public float poängtime = 5;
    public bool gepoäng;
    public Sprite[] numbers;
    public Text scoreText;
    public AudioSource musik;
    public AudioSource ljud;
    void Start()
    {
        camera = Camera.main;

        ljud = GetComponent<AudioSource>();
        loseScreen.SetActive(false);
    }

    void Update()
    {

        x += Time.deltaTime;
        timer += Time.deltaTime;

        scoreText.text = score.ToString();



        if (timer > Random.Range(2, 10)) 
        {
            print("skapa mullvad");
            timer = 0;
            int nummer = Random.Range(0, spawns.Length);;
            nymull = Instantiate(mulls[Random.Range(0, mulls.Length)], spawns[nummer].position, Quaternion.identity);
            nymull.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            full[nummer] = true;
            gepoäng = true;
        }

        if (x > 10)
        {
            x = x % 10;
            print("det funkar");
        }

        if (liv <= 0)
        {
            GameOver(); // Extrahera till metod
        }

        print("hundra: " + score % 100);
        print("tio: " + score % 10);
        print("ett: " + score % 1);


        if (Input.GetMouseButtonDown(0)) // Du klickar en knapp
        {
            var v3 = Input.mousePosition;
            v3.z = 10.0f;
            v3 = Camera.main.ScreenToWorldPoint(v3);

            for (int i = 0; i < spawns.Length; i++)
            {
                if (Vector3.Distance(nymull.transform.position, v3) < range)
                {
                    ljud.PlayOneShot(ljud.clip);
                    Destroy(nymull);
                    full[i] = false;
                    print("test");

                    if (gepoäng == true)
                    {
                        if (poängtime > 3)
                        {
                            score += 5;
                            gepoäng = false;
                            poängtime = 5;
                        }
                        if (poängtime > 1)
                        {
                            score += 3;
                            gepoäng = false;
                            poängtime = 5;
                        } 
                        else
                        {
                            score += 1;
                            gepoäng = false;
                            poängtime = 5;
                        }
                    }
                }
                else
                {
                    liv--;
                    print("miss");
                }
            }
        }

        if (gepoäng)
        {
            poängtime -= Time.deltaTime;
        }
    }

    private void GameOver()
    {
        musik.Stop();
        ljud.PlayOneShot(ljud.clip);
        Time.timeScale = 0;
        loseScreen.SetActive(true);

        print("Game Over");
    }
}
