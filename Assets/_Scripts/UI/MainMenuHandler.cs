using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public static MainMenuHandler Instance;

    [SerializeField] RectTransform _main;
    [SerializeField] RectTransform _levelSelector;
    [SerializeField] RectTransform _difficultySelector;
    [SerializeField] RectTransform _controls;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    public void OnMainToLevelSelector()
    {
        SceneManager.LoadScene("Level Selector");
    }

    public void OnMainToControls()
    {
        _main.localScale = Vector3.zero;
        _controls.localScale = Vector3.one;
    }

    public void OnLevelSelectorToMain()
    {
        GameController.SetLevel(null);

        SceneManager.LoadScene("Main Menu");
    }

    public void OnLevelSelectorToDifficultySelector()
    {
        _levelSelector.localScale = Vector3.zero;
        _difficultySelector.localScale = Vector3.one;
    }

    public void OnDifficultySelectorToLevelSelector()
    {
        GameController.SetDifficulty(Difficulty.NONE);

        _difficultySelector.localScale = Vector3.zero;
        _levelSelector.localScale = Vector3.one;
    }
}