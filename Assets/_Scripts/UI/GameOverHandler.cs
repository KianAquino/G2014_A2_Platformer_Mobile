using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _melonText;
    [SerializeField] Transform _melonCounter;
    [SerializeField] GameObject _melonImagePrefab;
    [SerializeField] GameObject _nextLevelButton;

    private int _playerMelons = 0;

    private void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        // MELON COUNTER ============================================
        _playerMelons = GameController.PlayerStats.Points;
        _melonText.text = $"MELONS COLLECTED\n{_playerMelons}";
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

        // NEXT LEVEL
        if (GameController.GameOverState == GameOverState.WON) _nextLevelButton.SetActive(true);
    }

    public void OnMainMenuClicked()
    {
        AudioSystem.PlaySFX(SFXType.CLICK, 0.3f);
        GameController.MainMenu();
    }

    public void OnReplayLevel()
    {
        AudioSystem.PlaySFX(SFXType.CLICK, 0.3f);
        GameController.LoadLevel();
    }

    public void OnNextLevel()
    {
        AudioSystem.PlaySFX(SFXType.CLICK, 0.3f);
        SceneManager.LoadScene("Level Selector");
    }
}
