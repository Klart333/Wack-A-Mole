using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    private AudioSource hitSound;

    private float hitCoolDown = 0.5f;
    private float coolDownTimer;

    void Awake()
    {
        hitSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        coolDownTimer += Time.deltaTime;

        if (ShouldPunch())
        {
            coolDownTimer = 0;

            hitSound.PlayOneShot(hitSound.clip);

            Vector3 position = GetWorldPointClicked();

            IClickable target = null;
            target = CheckForMouseHit(position, target);

            if (target == null)
            {
                // MISS
            }
        }
    }

    private bool ShouldPunch()
    {
        return Input.GetMouseButtonDown(0) && coolDownTimer >= hitCoolDown;
    }

    private Vector3 GetWorldPointClicked()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f); // I don't understand why z has to be 10, and it has to be 10
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        return mousePos;
    }

    private IClickable CheckForMouseHit(Vector3 position, IClickable target)
    {
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
