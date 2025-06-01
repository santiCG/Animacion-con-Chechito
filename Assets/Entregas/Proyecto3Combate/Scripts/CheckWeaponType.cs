using UnityEngine;

public class CheckWeaponType : MonoBehaviour
{
    [SerializeField] private GameObject shortWeapon;
    [SerializeField] private GameObject longWeapon;

    private Animator animator;

    public RuntimeAnimatorController lightWeaponAnimController;
    public RuntimeAnimatorController heavyWeaponAnimController;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float longWeaponLayerWeight = shortWeapon.gameObject.activeSelf ? 0 : 1;
    }

    public void ChangeWeapon()
    {
        if(shortWeapon.gameObject.activeSelf)
        {
            animator.runtimeAnimatorController = heavyWeaponAnimController;
            shortWeapon.gameObject.SetActive(false);
            longWeapon.gameObject.SetActive(true);
        }
        else
        { 
            animator.runtimeAnimatorController = lightWeaponAnimController;
            longWeapon.gameObject.SetActive(false);
            shortWeapon.gameObject.SetActive(true);
        }
    }
}
