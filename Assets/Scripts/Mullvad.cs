using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mullvad : MonoBehaviour, IClickable
{
    public void OnClicked()
    {
        Spel.Instance.Slåljud.PlayOneShot(Spel.Instance.Slåljud.clip);

        print(CreateMullvad.Instance.fullPositions.Remove(gameObject.transform.position));
        

        if (Spel.Instance.mullvadStillAlive == true)
        {
            if (Spel.Instance.poängtime > 3)
            {
                Spel.Instance.score += 5;
            }
            if (Spel.Instance.poängtime > 1)
            {
                Spel.Instance.score += 3;
            }
            else
            {
                Spel.Instance.score += 1;
            }

            Spel.Instance.mullvadStillAlive = false;
            Spel.Instance.poängtime = 5;
        }
        Destroy(gameObject);
    }


}
