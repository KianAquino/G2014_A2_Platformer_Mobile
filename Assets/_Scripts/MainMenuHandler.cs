using UnityEngine;

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
        _main.localScale = Vector3.zero;
        _levelSelector.localScale = Vector3.one;
    }

    public void OnMainToControls()
    {
        _main.localScale = Vector3.zero;
        _controls.localScale = Vector3.one;
    }

    public void OnLevelSelectorToMain()
    {
        _levelSelector.localScale = Vector3.zero;
        _main.localScale = Vector3.one;

        GameController.SetLevel(null);
    }

    public void OnLevelSelectorToDifficultySelector()
    {
        _levelSelector.localScale = Vector3.zero;
        _difficultySelector.localScale = Vector3.one;
    }

    public void OnDifficultySelectorToLevelSelector()
    {
        _difficultySelector.localScale = Vector3.zero;
        _levelSelector.localScale = Vector3.one;
    }
}