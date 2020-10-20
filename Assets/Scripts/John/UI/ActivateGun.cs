using UnityEngine;
public class ActivateGun : MonoBehaviour, IClickable
{
    [SerializeField]
    private UIGunText gunText;
    public void OnClicked()
    {
        gunText.ActivateGun();
    }
}