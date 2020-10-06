using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MullvadPositions : MonoBehaviour
{
    public static List<Vector3> mullvadPosisitons = new List<Vector3>();

    private void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            mullvadPosisitons.Add(transform.GetChild(i).localPosition);
        }
    }
}
