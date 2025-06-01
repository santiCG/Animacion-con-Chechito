using UnityEngine;

public class AttackHitboxControllerAV : MonoBehaviour
{
    [SerializeField] private GameObject[] hitboxes;
    
    public void ToggleHitboxes(int attackId)
    {
        for (int hitboxId = 0; hitboxId < hitboxes.Length; hitboxId++)
        {
            hitboxes[attackId].gameObject.SetActive(true);
        }
    }

    public void CleanUpHitboxes()
    {
        foreach (GameObject colliders in hitboxes)
        {

           colliders.gameObject.SetActive(false);
           
        }

    }
}
