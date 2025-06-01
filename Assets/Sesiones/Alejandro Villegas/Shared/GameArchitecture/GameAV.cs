using UnityEngine;

public class GameAV : MonoBehaviour
{
    #region Singleton
    private static GameAV instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CreateGame()
    {
        GameObject gameGO = new GameObject("[GAME]");
        instance = gameGO.AddComponent<GameAV>();
        DontDestroyOnLoad(gameGO);
    }

    public static GameAV Instance
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

    private CharacterStateAV playerOne;
    private void CreatePlayer()
    {
        GameObject playerGO = new GameObject("[PLAYER 1]");
        playerOne = playerGO.AddComponent<CharacterStateAV>();
    }

    private void Awake()
    {
        CreatePlayer();
    }

    public CharacterStateAV PlayerOne { get { return playerOne; } }
}
