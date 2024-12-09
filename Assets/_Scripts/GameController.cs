using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    private static GameController _instance;

    #region VARIABLES
    [Header("Levels")]
    [SerializeField] List<LevelUnlocked> _levelsUnlocked = new List<LevelUnlocked>();

    public static List<LevelUnlocked> LevelsUnlocked => _instance._levelsUnlocked;

    [SerializeField] PlayerStats _playerStats = new PlayerStats();
    public static PlayerStats PlayerStats => _instance._playerStats;

    private string _levelName;
    private Difficulty _difficulty = Difficulty.NONE;

    public static string LevelName => _instance._levelName;
    public static Difficulty Difficulty => _instance._difficulty;

    private GameOverState _gameOverState = GameOverState.LOSS;

    public static GameOverState GameOverState => _instance._gameOverState;

    [Header("Pause System")]
    [SerializeField] GameObject _pauseUIPrefab;
    [SerializeField] KeyCode _pauseKey = KeyCode.Escape;
    [Tooltip("If these scenes are loaded, the game is unpausable.")]
    [SerializeField] List<string> _noPauseScenes = new List<string>();
    private GameObject _pauseUICopy;

    [Header("")]
    [SerializeField] GameObject _eventSystem;
    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        _eventSystem.SetActive(true);
    }

    private void Update()
    {
        GetKeyboardInputs();
    }

    public static void UnlockLevel(int level)
    {
        foreach (LevelUnlocked levelUnlocked in _instance._levelsUnlocked)
        {
            if (levelUnlocked.Level == level) levelUnlocked.Unlocked = true;
        }
    }

    public static void GameOver(GameOverState state)
    {
        _instance._gameOverState = state;
        SceneManager.LoadScene("Game Over");
    }

    public static void SetLevel(string sceneName) => _instance._levelName = sceneName;
    public static void SetDifficulty(Difficulty difficulty) => _instance._difficulty = difficulty;

    public static void LoadLevel()
    {
        SceneManager.LoadScene(_instance._levelName);

        switch (_instance._difficulty)
        {
            case Difficulty.EASY:
                _instance._playerStats.SetLives(3);
                break;
            case Difficulty.MEDIUM:
                _instance._playerStats.SetLives(2);
                break;
            case Difficulty.HARD:
                _instance._playerStats.SetLives(1);
                break;
        }

        _instance._playerStats.SetPoints(0);
    }

    private void GetKeyboardInputs()
    {
        // PAUSE SYSTEM
        if (Input.GetKeyDown(_pauseKey))
            PauseSystem();
    }

    private void PauseSystem()
    {
        // CHECK IF A "NO PAUSE" SCENE IS LOADED
        foreach (string sceneName in _noPauseScenes)
        {
            Scene scene = SceneManager.GetSceneByName(sceneName);
            if (scene.IsValid() && scene.isLoaded)
                return;
        }

        if (Time.timeScale == 1f)
            PauseGame();
        else
            ResumeGame();
    }

    public static void PauseGame()
    {
        Time.timeScale = 0f;
        _instance._pauseUICopy = Instantiate(_instance._pauseUIPrefab);
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1f;
        Destroy(_instance._pauseUICopy);
    }

    public static void MainMenu()
    {
        Time.timeScale = 1f;

        // _instance._playerStats.SetLives(0);
        _instance._playerStats.SetPoints(0);

        SceneManager.LoadScene("Main Menu");
    }
}

[System.Serializable]
public class PlayerStats
{
    #region WALKSPEED
    [SerializeField] float _baseWalkspeed = 1f;
    private float _walkspeedMultiplier = 1f;
    public float Walkspeed => _baseWalkspeed * _walkspeedMultiplier;
    #endregion

    #region JUMP STRENGTH
    [SerializeField] float _baseJumpStrength = 5f;
    private float _jumpStrengthMultiplier = 1f;
    public float JumpStrength => _baseJumpStrength * _jumpStrengthMultiplier;
    #endregion

    #region POINTS
    [SerializeField] int _points = 0;

    public int Points => _points;

    [HideInInspector] public UnityEvent OnPointsChanged;

    /// <summary>
    /// Adjusts the player's points by the specified amount. 
    /// Pass a positive number to add points or a negative number to subtract points.
    /// </summary>
    public void ModifyPointsBy(int points)
    {
        _points += points;

        OnPointsChanged?.Invoke();
    }

    public void SetPoints(int value) => _points = value;
    #endregion

    #region LIVES
    private int _lives = 0;
    public int Lives => _lives;

    [HideInInspector] public UnityEvent OnLivesChanged;

    public void SetLives(int value)
    {
        _lives = value;

        OnLivesChanged?.Invoke();

        if (value <= 0) GameController.GameOver(GameOverState.LOSS);
    }

    public void DecreaseLife()
    {
        _lives--;

        OnLivesChanged?.Invoke();

        if (_lives <= 0) GameController.GameOver(GameOverState.LOSS);
    }
    #endregion
}

public enum Difficulty
{
    NONE,
    EASY,
    MEDIUM,
    HARD
}

public enum GameOverState
{
    LOSS,
    WON
}

[System.Serializable]
public class LevelUnlocked
{
    public int Level;
    public bool Unlocked;
}