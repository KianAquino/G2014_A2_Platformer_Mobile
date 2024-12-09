using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuHandler : MonoBehaviour
{
    public static MainMenuHandler Instance;

    [SerializeField] RectTransform _main;
    [SerializeField] RectTransform _levelSelector;
    [SerializeField] RectTransform _difficultySelector;
    [SerializeField] RectTransform _controls;

    [SerializeField] Transform _levelsGrid;
    [SerializeField] GameObject _levelSelectablePrefab;

    [Header("")]
    [SerializeField] bool _isLevelSelector = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (!_isLevelSelector) return;

        List<LevelUnlocked> levelsUnlocked = GameController.LevelsUnlocked;

        foreach(LevelUnlocked levelUnlocked in levelsUnlocked)
        {
            if (levelUnlocked.Unlocked)
            {
                LevelSelectable newButton = Instantiate(_levelSelectablePrefab, _levelsGrid).GetComponent<LevelSelectable>();
                newButton.SetSceneName("Level " +  levelUnlocked.Level);

                TextMeshProUGUI newButtonTMPro = newButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
                newButtonTMPro.text = levelUnlocked.Level.ToString();
            }
        }
    }

    public void OnMainToLevelSelector()
    {
        AudioSystem.PlaySFX(SFXType.CLICK, 0.3f);
        SceneManager.LoadScene("Level Selector");
    }

    public void OnMainToControls()
    {
        _main.localScale = Vector3.zero;
        _controls.localScale = Vector3.one;

        AudioSystem.PlaySFX(SFXType.CLICK, 0.3f);
    }

    public void OnControlsToMain()
    {
        _main.localScale = Vector3.one;
        _controls.localScale = Vector3.zero;

        AudioSystem.PlaySFX(SFXType.CLICK, 0.3f);
    }

    public void OnLevelSelectorToMain()
    {
        GameController.SetLevel(null);
        AudioSystem.PlaySFX(SFXType.CLICK, 0.3f);
        SceneManager.LoadScene("Main Menu");
    }

    public void OnLevelSelectorToDifficultySelector()
    {
        _levelSelector.localScale = Vector3.zero;
        _difficultySelector.localScale = Vector3.one;
        AudioSystem.PlaySFX(SFXType.CLICK, 0.3f);
    }

    public void OnDifficultySelectorToLevelSelector()
    {
        GameController.SetDifficulty(Difficulty.NONE);
        AudioSystem.PlaySFX(SFXType.CLICK, 0.3f);
        _difficultySelector.localScale = Vector3.zero;
        _levelSelector.localScale = Vector3.one;
    }
}