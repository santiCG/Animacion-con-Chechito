using UnityEngine;
[DefaultExecutionOrder(-1)]
public class CharacterAV : MonoBehaviour
{
    Transform lockTarget;
    [SerializeField] private bool isAiming;

    public Transform LockTarget {  get { return lockTarget; } set { value = lockTarget; } }

    public bool IsAiming
    {
        get => isAiming;
        set => isAiming = value;
    }

    private void RegisterComponents()
    {
        foreach (ICharacterComponentAV characterComponent in GetComponentsInChildren<ICharacterComponentAV>())
        {
            characterComponent.ParentCharacter = this;
        }

    }

    private void Awake()
    {
        RegisterComponents();
        Cursor.lockState = CursorLockMode.Locked;
    }
}
