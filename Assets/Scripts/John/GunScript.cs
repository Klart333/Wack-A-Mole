using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : PooledMonoBehaviour
{
    [SerializeField]
    private int shots = 10;

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 position = Vector3Extensions.ReplaceWith(mousePos, z: -8);

        transform.position = position;

        if (Input.GetMouseButtonDown(0))
        {
            shots--;
            CheckForDisable();
        }
    }

    private void CheckForDisable()
    {
        if (shots <= 0)
        {
            PowerupManager.Instance.gunActive = false;
            shots = 10;

            gameObject.SetActive(false);

        }
    }
}
