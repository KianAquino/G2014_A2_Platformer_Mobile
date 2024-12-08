using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class GameController : MonoBehaviour
{
    private static GameController _instance;

    [SerializeField] PlayerStats _playerStats = new PlayerStats();
    public static PlayerStats PlayerStats => _instance._playerStats;

    private string _levelName;
    private Difficulty _difficulty = Difficulty.NONE;

    public static string LevelName => _instance._levelName;
    public static Difficulty Difficulty => _instance._difficulty;

    [Header("Pause System")]
    [SerializeField] GameObject _pauseUIPrefab;
    [SerializeField] KeyCode _pauseKey = KeyCode.Escape;
    [Tooltip("If these scenes are loaded, the game is unpausable.")]
    [SerializeField] List<string> _noPauseScenes = new List<string>();
    private GameObject _pauseUICopy;

    [Header("")]
    [SerializeField] GameObject _eventSystem;

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
    #endregion

    #region LIVES
    private int _lives = 0;
    public int Lives => _lives;

    [HideInInspector] public UnityEvent OnLivesChanged;

    public void SetLives(int value)
    {
        _lives = value;

        OnLivesChanged?.Invoke();

        if (value <= 0) SceneManager.LoadScene("Main Menu");
    }

    public void DecreaseLife()
    {
        _lives--;

        OnLivesChanged?.Invoke();

        if (_lives <= 0) SceneManager.LoadScene("Main Menu");
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