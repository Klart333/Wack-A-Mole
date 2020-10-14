using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : PooledMonoBehaviour
{
    [SerializeField]
    private int shots = 10;

    private Vector3 lastPosition = new Vector3();

    private float speed; 
    private void Update()
    {
        FollowMouse();
        GetSpeed();

        // StartCoroutine("SwingToGoal", speed);
        float currentZ = transform.eulerAngles.z;
        currentZ += speed;
        transform.eulerAngles = new Vector3(0, 0, currentZ);

        if (speed == 0)
        {
            if (currentZ < 0)
            {
                currentZ++;
            }
            else
            {
                currentZ--;
            }
        }

        print(currentZ);

        if (Input.GetMouseButtonDown(0))
        {
            shots--;
            CheckForDisable();
        }

        lastPosition = transform.position;
    }

    private void GetSpeed()
    {
        float distance = Mathf.Pow((lastPosition.x - transform.position.x), 2) + Mathf.Pow((lastPosition.y - transform.position.y), 2);
        distance = Mathf.Sqrt(distance);

        // We need the direction
        if (lastPosition.magnitude < transform.position.magnitude)
        {
            distance = -distance;
        }

        speed = distance / Time.deltaTime;
    }

    private void FollowMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 position = Vector3Extensions.ReplaceWith(mousePos, z: -8);
        transform.position = position;
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

    private IEnumerator SwingToGoal(float speed)
    {

        int times = 20;
        for (int i = 0; i < times; i++)
        {
            
        }
        yield return new WaitForSeconds(0.1f);

    }
}
