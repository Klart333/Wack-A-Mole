using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMullvad : MonoBehaviour
{
    public static CreateMullvad Instance;

    [SerializeField]
    private GameObject[] prefabs;

    [SerializeField]
    private List<Vector3> mullPositions = new List<Vector3>();

    public List<Vector3> fullPositions = new List<Vector3>();

    private float timer;

    private void Start()
    {
        mullPositions = MullvadPositions.mullvadPosisitons;
    }
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
        timer += Time.deltaTime;

        if (timer > Random.Range(2, 10))
        {
            timer = 0;
            InstantiateMullvad();
        }
    }

    private void InstantiateMullvad()
    {
        int mullPos;
        Vector3 position = new Vector3();
        do
        {
            mullPos = Random.Range(0, mullPositions.Count);
            position = mullPositions[mullPos];
        } while (fullPositions.Contains(mullPositions[mullPos]));

        Instantiate(prefabs[Random.Range(0, prefabs.Length)], position, Quaternion.identity);

        fullPositions.Add(mullPositions[mullPos]);

        Spel.Instance.mullvadStillAlive = true;
    }

}
