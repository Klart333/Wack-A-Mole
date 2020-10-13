using UnityEngine;
using TMPro;

public class UIActivateGun : MonoBehaviour
{
    private TextMeshProUGUI activateGunText;
    private Animator animator;

    private bool gunActivatible;
    private void Awake()
    {
        activateGunText = GetComponentInChildren<TextMeshProUGUI>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        GameManager.Instance.OnSharkKilled += CheckIfGun;
    }

    private void Update()
    {
        if (gunActivatible && Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    private void CheckIfGun(float timeToKill)
    {
        if (GameManager.Instance.hitSpree >= 20)
        {
            GunActivatible();
        }
        else
        {
            GunNotActivatible();
        }
    }

    private void GunActivatible()
    {
        animator.ResetTrigger("Reset");
        animator.SetTrigger("Breath");
        
        activateGunText.text = "Press Space To Activate GUN!";
        gunActivatible = true;
    }

    private void GunNotActivatible()
    {
        animator.ResetTrigger("Breath");
        animator.SetTrigger("Reset");

        activateGunText.text = "";
        gunActivatible = false;
    }
}
