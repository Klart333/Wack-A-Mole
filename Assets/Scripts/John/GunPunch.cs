using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPunch : MonoBehaviour // Basically the same as punchscript, with the exceptions of increase to hitspree and the sound effect played. I felt like this was a really clear way of doing things instead of placing checks in punchScript. 
{
    private UIHitSpree hitSpree;
    private PunchScript punchScript;
    private void Awake()
    {
        hitSpree = GameObject.Find("[TextManager]").GetComponent<UIHitSpree>(); // The guns are pooled so it should be fine, even though i don't like the GameObject.Find
        punchScript = GameManager.Instance.gameObject.GetComponent<PunchScript>();
    }
    private void OnEnable()
    {
        punchScript.enabled = false;
    }
    void Update()
    {
        if (ShouldPunch())
        {
            if (Punch() == true) // If we hit something
            {
                GameManager.Instance.hitSpree += 2; // The gun increases the hitspree with double
                hitSpree.UpdateHitSpree();
            }
            else
            {
                Audio.Instance.PlaySoundEffect("Gunshot", "ArcadeShot"); 

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

        Collider[] hitColliders = Physics.OverlapSphere(position, 0.2f);

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

    private void OnDisable()
    {
        punchScript.enabled = true;
    }
}
