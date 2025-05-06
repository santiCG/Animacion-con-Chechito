using UnityEngine;

public class AttackHitboxController : MonoBehaviour
{
    [SerializeField] private GameObject[] hitboxes;

    public void ToggleHitboxes(int attackID)
    {
        for (int hitboxID = 0; hitboxID < hitboxes.Length; hitboxID++)
        {
            GameObject hitbox = hitboxes[hitboxID];
            hitboxes[attackID].gameObject.SetActive(!hitbox.activeSelf);
        }
    }

    public void CleanupHitboxes()
    {
        foreach (GameObject col in hitboxes)
        {
            col.SetActive(false);
        }
    }
}
