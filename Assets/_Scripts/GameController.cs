using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    private static GameController _instance;

    [SerializeField] PlayerStats _playerStats;

    private string _levelName;
    private Difficulty _difficulty = Difficulty.NONE;

    public string LevelName => _levelName;
    public Difficulty Difficulty => _difficulty;

    [Header("Pause System")]
    [SerializeField] KeyCode _pauseKey = KeyCode.Escape;
    [Tooltip("If these scenes are loaded, the game is unpausable.")]
    [SerializeField] List<string> _noPauseScenes = new List<string>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        GetKeyboardInputs();
    }

    public static void SetLevel(string sceneName) => _instance._levelName = sceneName;
    public static void SetDifficulty(Difficulty difficulty) => _instance._difficulty = difficulty;

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
        Debug.Log("[Game Controller] Game paused.");
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1f;
        Debug.Log("[Game Controller] Game resumed.");
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

    #region POINTS
    [SerializeField] int _points = 0;
    public int Points => _points;
    /// <summary>
    /// Adjusts the player's points by the specified amount. 
    /// Pass a positive number to add points or a negative number to subtract points.
    /// </summary>
    public void ModifyPointsBy(int points) => _points += points;
    #endregion

    #region LIVES
    private int _lives = 0;
    public int Lives => _lives;

    public void SetLives(int value) => _lives = value;
    #endregion
}

public enum Difficulty
{
    NONE,
    EASY,
    MEDIUM,
    HARD
}