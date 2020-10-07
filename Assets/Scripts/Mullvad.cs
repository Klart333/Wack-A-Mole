using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mullvad : MonoBehaviour, IClickable
{
    public void OnClicked()
    {
        GameManager.Instance.Slåljud.PlayOneShot(GameManager.Instance.Slåljud.clip);

        CreateMullvad.Instance.fullPositions.Remove(gameObject.transform.position);
        

        if (GameManager.Instance.mullvadStillAlive == true)
        {
            if (GameManager.Instance.poängtime > 3)
            {
                GameManager.Instance.score += 5;
            }
            if (GameManager.Instance.poängtime > 1)
            {
                GameManager.Instance.score += 3;
            }
            else
            {
                GameManager.Instance.score += 1;
            }

            GameManager.Instance.mullvadStillAlive = false;
            GameManager.Instance.poängtime = 5;
        }
        Destroy(gameObject);
    }


}
