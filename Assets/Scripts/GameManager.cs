using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // [SerializeField] private TileDragHandler tileDragHandler;
    [SerializeField] private SettingsManager settingsManager;
    public GameActions controls;

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
        controls = new GameActions();
        controls.Enable();
    }
}

public enum GameState
{
    Idle,
    Searching,
    Fighting,
    InGrid
}