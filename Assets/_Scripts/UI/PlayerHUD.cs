using Unity.VisualScripting;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Transform _lifeCounter;
    [SerializeField] GameObject _heartImagePrefab;
    [SerializeField] Transform _melonCounter;
    [SerializeField] GameObject _melonImagePrefab;

    private int _playerLives = 0;
    private int _playerMelons = 0;

    private void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        // LIFE COUNTER ============================================
        _playerLives = GameController.PlayerStats.Lives;
        // Clear Counter
        for(int i = 0; i < _lifeCounter.childCount; i++)
        {
            Transform child = _lifeCounter.GetChild(i);
            Destroy(child.gameObject);
        }
        // Re-populate
        for (int i = 0; i < _playerLives;  i++)
        {
            Instantiate(_heartImagePrefab, _lifeCounter);
        }
        // MELON COUNTER ============================================
        _playerMelons = GameController.PlayerStats.Points;
        // Clear Counter
        for (int i = 0; i < _melonCounter.childCount; i++)
        {
            Transform child = _melonCounter.GetChild(i);
            Destroy(child.gameObject);
        }
        // Re-populate
        for (int i = 0; i < _playerMelons; i++)
        {
            Instantiate(_melonImagePrefab, _melonCounter);
        }
    }

    private void OnEnable()
    {
        GameController.PlayerStats.OnLivesChanged.AddListener(RefreshUI);
        GameController.PlayerStats.OnPointsChanged.AddListener(RefreshUI);
    }

    private void OnDisable()
    {
        GameController.PlayerStats.OnLivesChanged.RemoveListener(RefreshUI);
        GameController.PlayerStats.OnPointsChanged.RemoveListener(RefreshUI);
    }
}
