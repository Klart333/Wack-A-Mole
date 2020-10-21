using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSpawner : MonoBehaviour
{
    public static SharkSpawner Instance;

    [SerializeField]
    private Shark[] prefabs; // Array of shark prefabs, accessed through the script

    private float spawnTimer;
    private int sortingLayerNum = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (ShouldSpawn()) 
        {
            spawnTimer = 0;
            SpawnShark();
        }
    }

    private bool ShouldSpawn()
    {
        float spawnTime;
        if (GameManager.Instance.DifficultyMultiplier <= 5)
        {
            spawnTime = Random.Range(2, 5) / GameManager.Instance.DifficultyMultiplier;
        }
        else
        {
            spawnTime = Random.Range(0.2f, 0.5f) / Mathf.Log10(GameManager.Instance.DifficultyMultiplier);
        }

        print("Spawn Time: " + spawnTime);
        print("Difficulty: " + GameManager.Instance.DifficultyMultiplier);

        return (spawnTimer >= spawnTime) && (GameManager.Instance.Gameover == false);
    }

    private void SpawnShark()
    {
        Vector3 position = RandomScreenEdgeToWorldPoint();
        Shark prefab = prefabs[Random.Range(0, prefabs.Length)];

        Shark shark = prefab.GetAtPosAndRot<Shark>(position, prefab.gameObject.transform.rotation); // We call the inherited method 'Get' which asks the Pool for a GameObject from the queue and then makes it active
        shark.SetRoamGoal();

        shark.GetComponent<SpriteRenderer>().sortingOrder = sortingLayerNum--;
    }
    
    private static Vector3 RandomScreenEdgeToWorldPoint()
    {
        // The screen is 1920 by 1080, remove some for margin, except for on the x where we want the max or min
        float xPos = Random.Range(1, 3) == 1 ? xPos = 0f : xPos = 1920f; // Basically a coin flip, 3 is not included 
        float yPos = Random.Range(100, 900);
        if (xPos == 1920) // The shark can't be allowed spawn over the camera
        {
            yPos = Random.Range(300, 900);
        }

        Vector2 randomScreenPos = new Vector2(xPos, yPos); 
        Vector3 position = Camera.main.ScreenToWorldPoint(randomScreenPos);
        position.z = 0;
        return position;
    }
}
