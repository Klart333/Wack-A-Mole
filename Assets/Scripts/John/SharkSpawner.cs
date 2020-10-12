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

        return (spawnTimer >= spawnTime) && (GameManager.Instance.GameOvering == false);
    }

    private void SpawnShark()
    {
        Vector3 position = RandomScreenToWorldPoint();
        Shark prefab = prefabs[Random.Range(0, prefabs.Length)];

        var shark = prefab.GetAtPosAndRot<Shark>(position, prefab.gameObject.transform.rotation); // We call the inherited method 'Get' which asks the Pool for a GameObject from the queue and then makes it active

        shark.GetComponent<SpriteRenderer>().sortingOrder = sortingLayerNum--;
    }

    private static Vector3 RandomScreenToWorldPoint()
    {
        Vector2 randomScreenPos = new Vector2(Random.Range(120, 1800), Random.Range(100, 900)); // The screen is 1920 by 1080, remove some for margin
        Vector3 position = Camera.main.ScreenToWorldPoint(randomScreenPos);
        position.z = 0;
        return position;
    }
}
