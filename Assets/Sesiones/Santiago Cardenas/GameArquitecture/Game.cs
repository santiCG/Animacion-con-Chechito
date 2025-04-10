using UnityEngine;

public class Game : MonoBehaviour
{
    #region Singleton
    private static Game instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CreateGame()
    {
        GameObject gameGo = new GameObject("[TheGame]");
        instance = gameGo.AddComponent<Game>();
        DontDestroyOnLoad(gameGo);
    }
    public static Game Instance
    { 
        get 
        { 
            if (instance == null)
            {
                CreateGame();
            }
            return instance;
        }
    }
    #endregion

    private ChracterStateALR playerOne;
    private void CreatePlayer()
    {
        GameObject playerGo = new GameObject("[Player1]");
        playerOne = playerGo.AddComponent<ChracterStateALR>();
        DontDestroyOnLoad(playerGo);
    }

    public ChracterStateALR PlayerOne => playerOne;

    private void Awake()
    {
        CreatePlayer();
    }
}
