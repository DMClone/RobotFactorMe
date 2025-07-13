using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private SettingsManager settingsManager;
    public GameState CurrentGameState { get; private set; } = GameState.Idle;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

public enum GameState
{
    Idle,
    Searching,
    Fighting,
    InGrid
}