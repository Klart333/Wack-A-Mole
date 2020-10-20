﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickScript : MonoBehaviour
{
    private UIHitSpree hitSpreeScript;

    private void Awake()
    {
        hitSpreeScript = FindObjectOfType<UIHitSpree>();
    }
    void Update()
    {
        if (ShouldClick())
        {
            Audio.Instance.PlaySoundEffect("Punch", "Punch"); // Whether he hit or miss, works with the gun because this script is disabled
            if (Click() == true) // If we hit something
            {

            }
            else
            {
                GameManager.Instance.hitSpree = 0;
                hitSpreeScript.UpdateHitSpree();
            }

        }
    }
    private bool ShouldClick()
    {
        return ((Input.touchCount != 0 || Input.GetMouseButton(0)) && GameManager.Instance.GameOvering == false);
    }

    private bool Click()
    {
        Vector3 position = GetWorldPointClicked();

        IClickable target = null;
        target = TryClickAtPosition(position);

        return (target != null); // We hit something if target isn't null
    }

    private Vector3 GetWorldPointClicked()
    {
        Vector3 clickPos = new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, 10f); // I don't understand why z has to be 10, but it has to be 10
        clickPos = Camera.main.ScreenToWorldPoint(clickPos);
        return clickPos;
    }

    private IClickable TryClickAtPosition(Vector3 position)
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
