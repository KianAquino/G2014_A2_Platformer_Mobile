using UnityEditor.Build.Reporting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance;

    [Header("Keyboard Inputs")]
    [SerializeField] KeyCode _pauseKey = KeyCode.Escape;

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

    private void GetKeyboardInputs()
    {
        // PAUSE SYSTEM
        if (Input.GetKeyDown(_pauseKey))
        {
            if (Time.timeScale == 1f)
                PauseGame();
            else
                ResumeGame();
        }
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
}