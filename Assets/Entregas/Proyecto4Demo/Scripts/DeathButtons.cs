using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathButtons : MonoBehaviour
{
    public void Reload()
    {
        SceneManager.LoadScene(0);
    }

    public void StopIt()
    {
        Application.Quit();
    }
}
