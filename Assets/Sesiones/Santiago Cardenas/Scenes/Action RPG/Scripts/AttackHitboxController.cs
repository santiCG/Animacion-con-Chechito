using UnityEngine;

public class AttackHitboxController : MonoBehaviour
{
    [SerializeField] private GameObject[] hitboxes;

    public void ToggleHitboxes(int attackID)
    {

        GameObject hitbox = hitboxes[attackID];
        hitboxes[attackID].gameObject.SetActive(!hitbox.activeSelf);
        
        //for (int hitboxID = 0; hitboxID < hitboxes.Length; hitboxID++)
        //{

        //}
    }

    public void CleanupHitboxes()
    {
        foreach (GameObject col in hitboxes)
        {
            col.SetActive(false);
        }
    }
}
