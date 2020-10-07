using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSpawner : MonoBehaviour
{
    public static SharkSpawner Instance;

    [SerializeField]
    private GameObject[] prefabs;

    private float spawnTimer;

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
        if (spawnTimer > (Random.Range(2, 5) / GameManager.Instance.difficultyMultiplier)) // Sharks spawn faster over time
        {
            spawnTimer = 0;
            SpawnShark();
        }
    }

    private void SpawnShark()
    {
        print("Trying To Spawn");
        Vector3 position = RandomScreenToWorldPoint();

        Instantiate(prefabs[Random.Range(0, prefabs.Length)], position, Quaternion.identity);
    }

    private static Vector3 RandomScreenToWorldPoint()
    {
        Vector2 randomScreenPos = new Vector2(Random.Range(120, 1800), Random.Range(100, 900)); // The screen is 1920 by 1080, remove some for margin
        Vector3 position = Camera.main.ScreenToWorldPoint(randomScreenPos);
        position.z = 0;
        return position;
    }
}
