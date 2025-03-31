using UnityEngine;
[DefaultExecutionOrder(-1)]
public class CharacterAVP : MonoBehaviour
{
    Transform lockTarget;
    [SerializeField] private bool isAiming;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isRunning;

    public Transform LockTarget {  get { return lockTarget; } set { value = lockTarget; } }

    public bool IsAiming
    {
        get => isAiming;
        set => isAiming = value;
    }

    public bool IsGrounded { get {  return isGrounded; } set {  isGrounded = value; } }
    public bool IsRunning { get {  return isRunning; } set {  isRunning = value; } }

    private void RegisterComponents()
    {
        foreach (ICharacterComponentAVP characterComponent in GetComponentsInChildren<ICharacterComponentAVP>())
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
