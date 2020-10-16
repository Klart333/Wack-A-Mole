﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchScript : MonoBehaviour
{
    [SerializeField]
    private UIHitSpree hitSpree;

    void Update()
    {
        if (ShouldPunch())
        {
            if (Punch() == true) // If we hit something
            {
                GameManager.Instance.hitSpree++;
                hitSpree.UpdateHitSpree();
            }
            else
            {
                Audio.Instance.PlaySoundEffect("Punch", "Punch"); // Incase we miss, we can safely play the punch sound, we don't want to always play it. And this will work with the gun because when the gun is active this script is disabled

                GameManager.Instance.hitSpree = 0;
                hitSpree.UpdateHitSpree();
            }

        }
    }
    private bool ShouldPunch()
    {
        return (Input.GetMouseButtonDown(0) && GameManager.Instance.GameOvering == false);
    }

    private bool Punch()
    {
        Vector3 position = GetWorldPointClicked();

        IClickable target = null;
        target = TryPunchAtPosition(position);

        return (target != null); // We hit something if target isn't null
    }

    private Vector3 GetWorldPointClicked()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f); // I don't understand why z has to be 10, and it has to be 10
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        return mousePos;
    }

    private IClickable TryPunchAtPosition(Vector3 position)
    {
        IClickable target = null;

        Collider[] hitColliders = Physics.OverlapSphere(position, 0.1f);

        foreach (Collider hit in hitColliders)
        {
            target = hit.GetComponent<IClickable>();
            if (target != null)
            {
                target.OnClicked();
            }
        }

        return target;
    }


}
