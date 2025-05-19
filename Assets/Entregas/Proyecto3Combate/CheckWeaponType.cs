using UnityEngine;

public class CheckWeaponType : MonoBehaviour
{
    [SerializeField] private GameObject shortWeapon;
    [SerializeField] private GameObject longWeapon;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float longWeaponLayerWeight = shortWeapon.gameObject.activeSelf ? 0 : 1;
        animator.SetLayerWeight(1, longWeaponLayerWeight);
    }

    public void ChangeWeapon()
    {
        if(shortWeapon.gameObject.activeSelf)
        {
            shortWeapon.gameObject.SetActive(false);
            longWeapon.gameObject.SetActive(true);
        }
        else
        { 
            longWeapon.gameObject.SetActive(false);
            shortWeapon.gameObject.SetActive(true);
        }
    }
}
