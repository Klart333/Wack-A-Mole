using UnityEngine;
using TMPro;

public class UIActivateGun : MonoBehaviour
{
    [SerializeField]
    private GunScript gunPrefab;

    private TextMeshProUGUI activateGunText;
    private Animator animator;

    private bool gunActivatible;

    private float gunCooldown = 20f;
    private float coolDownTimer;
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
        coolDownTimer += Time.deltaTime;

        if (gunActivatible && Input.GetKeyDown(KeyCode.Space))
        {
            ActivateGun();
        }
    }

    private void CheckIfGun(float timeToKill)
    {
        if (GameManager.Instance.hitSpree >= 19 && coolDownTimer >= gunCooldown)
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

    private void ActivateGun()
    {
        coolDownTimer = 0;

        gunPrefab.Get<GunScript>();
        PowerupManager.Instance.gunActive = true;
    }
}
